using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using Gir;
using Scriban;
using Scriban.Runtime;

namespace Generator
{
    public interface IGenerator
    {
        void Generate();
    }

    public enum StructType
    {
        RefStruct,          // Simple Marshal-able C-struct
        OpaqueStruct,       // Opaque struct, marshal as class + IntPtr
        PublicClassStruct,  // GObject type struct (special case)
        PrivateClassStruct  // Same as above, but opaque
    }

    public abstract class Generator<K> : IGenerator where K : ITemplateLoader
    {
        #region Fields

        private readonly TypeResolver typeResolver;
        private readonly string dllImport;

        #endregion

        #region Properties

        private GRepository Repository { get; }
        private Project Project { get; }

        private ScriptObject? scriptObject;
        protected ScriptObject ScriptObject 
            => scriptObject ??= CreateScriptObject(Repository.Namespace.Name, dllImport);

        #endregion Properties

        #region Constructors

        protected Generator(Project project)
        {
            Project = project ?? throw new ArgumentNullException(nameof(project));
            
            Repository = ReadRepository(project.Gir);
            
            dllImport = GetDllImport(project) ?? throw new ArgumentNullException(nameof(dllImport));
            var aliases = new List<GAlias>();
            aliases.AddRange(Repository.Namespace?.Aliases ?? Enumerable.Empty<GAlias>());
            var aliasResolver = new AliasResolver(aliases);
            typeResolver = new TypeResolver(aliasResolver);
        }

        #endregion
        
        // Determines the dll name from the shared library (based on msys2 gtk binaries)
        // SEE: https://tldp.org/HOWTO/Program-Library-HOWTO/shared-libraries.html
        private static string GetDllImport(Project project)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return project.GetWindowsDllImport();
            else
                return project.GetLinuxDllImport();
        }

        /// <summary>
        /// Determine how we should generate a given record/struct based on
        /// a simple set of rules.
        /// </summary>
        protected StructType GetStructType(GRecord record)
        {
            switch (record)
            {
                // Disguised (private) Class Struct
                case GRecord r when r.GLibIsGTypeStructFor != null && r.Disguised == true:
                    return StructType.PrivateClassStruct;

                // Introspectable (public) Class Struct
                case GRecord r when r.GLibIsGTypeStructFor != null && r.Disguised == false:
                    return StructType.PublicClassStruct;

                // Regular C-Style Structure
                case GRecord r when !r.Disguised && r.Fields.Count > 0:
                    return StructType.RefStruct;
                
                // Default: Disguised Struct (Marshal with IntPtr)
                default:
                    return StructType.OpaqueStruct;
            }
        }

        public void Generate()
        {
            if (Repository.Namespace is null)
                throw new Exception($"Can not generate for {Project.Name}. Namespace is missing.");
            
            if (Repository.Namespace.Name is null)
                throw new Exception("Could not create code. Namespace is missing a name.");

            GenerateClasses(Repository.Namespace.Classes, Repository.Namespace.Name);
            scriptObject = null; //Reset script object to create a new for a new run
            GenerateStructs(Repository.Namespace.Records, Repository.Namespace.Name);
            scriptObject = null; //Reset script object to create a new for a new run
            GenerateStructs(Repository.Namespace.Unions, Repository.Namespace.Name);
            scriptObject = null; //Reset script object to create a new for a new run
            GenerateEnums(Repository.Namespace.Bitfields, Repository.Namespace.Name, true);
            scriptObject = null; //Reset script object to create a new for a new run
            GenerateEnums(Repository.Namespace.Enumerations, Repository.Namespace.Name, false);
            scriptObject = null; //Reset script object to create a new for a new run
            GenerateDelegates(Repository.Namespace.Callbacks, Repository.Namespace.Name);
            scriptObject = null; //Reset script object to create a new for a new run
        }

        protected virtual void GenerateClasses(IEnumerable<GInterface> classes, string @namespace) { }
        protected virtual void GenerateStructs(IEnumerable<GRecord> records, string @namespace) { }
        protected virtual void GenerateEnums(IEnumerable<GEnumeration> enums, string @namespace, bool hasFlags) { }
        protected virtual void GenerateDelegates(IEnumerable<GCallback> delegates, string @namespace) { }
        
        private static GRepository ReadRepository(string girFile)
        {
            var serializer = new XmlSerializer(typeof(GRepository), "http://www.gtk.org/introspection/core/1.0");

            using var fs = new FileStream(girFile, FileMode.Open);
            return (GRepository) serializer.Deserialize(fs);
        }

        protected void Generate<T>(T obj, string templateName, string subfolder,
            string? fileName, ScriptObject scriptObject)
        {
            //Create subfolder if it does not exist
            Directory.CreateDirectory(Path.Combine(Project.Folder, subfolder));
            
            if (string.IsNullOrEmpty(fileName))
            {
                Console.WriteLine($"Could not generate {templateName}, name is missing");
                return;
            }
            
            scriptObject.Import(obj);

            fileName = Path.Combine(subfolder, fileName + ".Generated.cs");

            var loader = Activator.CreateInstance<K>();
            var context = new TemplateContext {TemplateLoader = loader};
            context.PushGlobal(scriptObject);
            
            var templateFile = loader.GetPath(null, default, templateName + ".sbntxt");
            if (fileName.Contains("Accessible"))
            {
                //TODO: Workaround for missing ATK!
                Console.WriteLine(
                    $"Skipping file {fileName} because it looks like an ATK class which is not supported.");
                return;
            }
            
            GenerateCode(templateFile, fileName, context);
        }
        
        private void GenerateCode(string templateFile, string fileName, TemplateContext context)
        {
            try
            {
                var template = Template.Parse(File.ReadAllText(templateFile));
                var content = template.Render(context);
                Write(fileName, content);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Could not create code for {fileName}: {ex.Message}");
            }
        }

        private void Write(string fileName, string content)
        {
            var path = Path.Combine(Project.Folder, fileName);
            File.WriteAllText(path, content);
        }

        private ScriptObject CreateScriptObject(string @namespace, string dllImport)
        {
            var scriptObject = new ScriptObject
            {
                {"namespace", @namespace},
                {"dll_import", dllImport}
            };
            scriptObject.Import("comment_line_by_line_with_prefix",
                new Func<string, string, string>((s, prefix) => s.CommentLineByLine(prefix))
            );
            scriptObject.Import("make_pascal_case",
                new Func<string, string>((s) => s.MakePascalCase())
            );
            scriptObject.Import("make_single_line",
                new Func<string, string>((s) => s.MakeSingleLine())
            );
            scriptObject.Import("escape_quotes",
                new Func<string, string>((s) => s.EscapeQuotes())
            );
            scriptObject.Import("type_to_string",
                new Func<GType?, string?>((t) =>
                {
                    try
                    {
                        return t is null ? null : typeResolver.GetTypeString(t).ToString();
                    }
                    catch
                    {
                        return null;
                    }
                })
            );
            scriptObject.Import("debug",
                new Action<string>(Console.WriteLine)
            );
            scriptObject.Import("fix_identifier",
                new Func<string, string>((s) => s.FixIdentifier())
            );
            scriptObject.Import("resolve_type",
                new Func<IType, string>((t) =>
                {
                    var resolvedType = typeResolver.Resolve(t);
                    return resolvedType.GetTypeString();
                })
            );
            scriptObject.Import("resolve_field",
                new Func<IType, string>((t) =>
                {
                    var resolvedType = typeResolver.Resolve(t);
                    return resolvedType.GetFieldString();
                })
            );
            return scriptObject;
        }
    }
}