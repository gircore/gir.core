using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Gir;
using Scriban;
using Scriban.Runtime;

namespace Generator
{
    public interface IGenerator
    {
        #region Properties
        
        public bool GenerateComments { get; set; }
        
        #endregion
        
        #region Methods

        void Generate();

        #endregion
    }

    public enum StructType
    {
        RefStruct, // Simple Marshal-able C-struct
        OpaqueStruct, // Opaque struct, marshal as class + IntPtr
        PublicClassStruct, // GObject type struct (special case)
        PrivateClassStruct // Same as above, but opaque
    }

    public abstract class Generator<K> : IGenerator where K : ITemplateLoader
    {
        #region Fields

        private readonly TypeResolver _typeResolver;
        private string? _dllImport;

        #endregion

        #region Properties

        public bool GenerateComments { get; set; }

        private GRepository Repository { get; }
        private Project Project { get; }

        #endregion

        #region Constructors

        protected Generator(Project project)
        {
            Project = project ?? throw new ArgumentNullException(nameof(project));

            Repository = ReadRepository(project.Gir);
            List<GAlias> aliases = ProcessAliases(Repository);

            var aliasResolver = new AliasResolver(aliases);
            _typeResolver = new TypeResolver(aliasResolver);

            FixRepository(Repository);
        }

        private static List<GAlias> ProcessAliases(GRepository? repo)
        {
            List<GAlias> aliases = new List<GAlias>();
            aliases.AddRange(repo?.Namespace?.Aliases ?? Enumerable.Empty<GAlias>());
            ProcessAliasesRecursive(repo?.Namespace?.Name, repo?.Includes, ref aliases);
            return aliases;
        }

        private static void ProcessAliasesRecursive(string? repoName, List<GInclude>? includes, ref List<GAlias> aliases)
        {
            if (repoName == null)
                return;

            // TODO: This is incredibly inefficient. We already need to load dependencies, so
            // we should incrementally gather data and reuse it, rather than processing every
            // dependency an extra time per library. Do this during the generator rewrite.
            foreach (GInclude include in (includes ?? new List<GInclude>()))
            {
                try
                {
                    string filename = include.Name + "-" + include.Version + ".gir";
                    GRepository? dependency = ReadRepository("../gir-files/" + filename);
                    
                    // Recursively process aliases for all includes
                    ProcessAliasesRecursive(dependency?.Namespace?.Name, dependency?.Includes, ref aliases);
                    
                    // Also add toplevel aliases
                    aliases.AddRange(dependency?.Namespace?.Aliases ?? Enumerable.Empty<GAlias>());
                }
                catch { }
            }
        }

        #endregion

        #region Methods

        private static GRepository ReadRepository(string girFile)
        {
            var serializer = new XmlSerializer(typeof(GRepository), "http://www.gtk.org/introspection/core/1.0");

            using var fs = new FileStream(girFile, FileMode.Open);
            object? repository = serializer.Deserialize(fs);
            
            return repository as GRepository ?? throw new Exception($"Could not deserialize {girFile}");
        }

        protected ScriptObject GetScriptObject()
            => CreateScriptObject(Repository.Namespace!.Name!, _dllImport!);

        /// <summary>
        /// Determine how we should generate a given record/struct based on
        /// a simple set of rules.
        /// </summary>
        protected StructType GetStructType(GRecord record)
        {
            return record switch
            {
                // Disguised (private) Class Struct
                { Disguised: true, GLibIsGTypeStructFor: { } } => StructType.PrivateClassStruct,

                // Introspectable (public) Class Struct
                { Disguised: false, GLibIsGTypeStructFor: { } } => StructType.PublicClassStruct,

                // Regular C-Style Structure
                { Disguised: false, Fields: { } f } when f.Count > 0 => StructType.RefStruct,

                // Default: Disguised Struct (Marshal with IntPtr)
                _ => StructType.OpaqueStruct
            };
        }

        public void Generate()
        {
            if (Repository.Namespace is null)
                throw new Exception($"Can not generate for {Project}. Namespace is missing.");

            if (Repository.Namespace.Name is null)
                throw new Exception("Could not create code. Namespace is missing a name.");

            _dllImport = Repository.Namespace.GetDllImport(Repository.Namespace.Name) ?? throw new ArgumentNullException(nameof(_dllImport));

            GenerateClasses(Repository.Namespace.Classes, Repository.Namespace.Name);
            GenerateInterfaces(Repository.Namespace.Interfaces, Repository.Namespace.Name);
            GenerateStructs(Repository.Namespace.Records, Repository.Namespace.Name);
            GenerateStructs(Repository.Namespace.Unions, Repository.Namespace.Name);
            GenerateEnums(Repository.Namespace.Bitfields, Repository.Namespace.Name, true);
            GenerateEnums(Repository.Namespace.Enumerations, Repository.Namespace.Name, false);
            GenerateDelegates(Repository.Namespace.Callbacks, Repository.Namespace.Name);
            GenerateGlobals(Repository.Namespace.Functions, Repository.Namespace.Name);
            GenerateConstants(Repository.Namespace.Constants, Repository.Namespace.Name);
        }

        protected virtual void GenerateInterfaces(IEnumerable<GInterface> interfaces, string @namespace) { }
        protected virtual void GenerateClasses(IEnumerable<GClass> classes, string @namespace) { }
        protected virtual void GenerateStructs(IEnumerable<GRecord> records, string @namespace) { }
        protected virtual void GenerateEnums(IEnumerable<GEnumeration> enums, string @namespace, bool hasFlags) { }
        protected virtual void GenerateDelegates(IEnumerable<GCallback> delegates, string @namespace) { }
        protected virtual void GenerateGlobals(IEnumerable<GMethod> methods, string @namespace) { }
        protected virtual void GenerateConstants(IEnumerable<GConstant> constants, string @namespace) { }

        private void FixRepository(GRepository repository)
        {
            MarkNewMethodsAsNew(repository);
        }

        private void MarkNewMethodsAsNew(GRepository repository)
        {
            if (repository.Namespace is null)
                return;

            foreach (GClass? cls in repository.Namespace.Classes)
            {
                if (string.IsNullOrEmpty(cls.Parent))
                    continue;

                GClass? parent = repository.Namespace.Classes.Find(x => x.Name == cls.Parent);

                if (parent is null)
                    continue;

                foreach (GMethod? method in cls.AllMethods)
                {
                    RecursivelyAddNewIdentifierToMethod(method, parent, repository.Namespace.Classes);
                }
            }
        }

        private void RecursivelyAddNewIdentifierToMethod(GMethod method, GClass parentClass, List<GClass> classes)
        {
            method.IsNew = parentClass.AllMethods.Any(x =>
                x.Name == method.Name &&
                x.Parameters.AreEqual(_typeResolver, method.Parameters)
            );

            if (method.IsNew || string.IsNullOrEmpty(parentClass.Parent))
                return;

            GClass? parent = classes.Find(x => x.Name == parentClass.Parent);
            if (parent is null)
                return;

            RecursivelyAddNewIdentifierToMethod(method, parent, classes);
        }

        protected void Generate(string templateName, string subfolder,
            string? fileName, ScriptObject scriptObject)
        {
            //Create subfolder if it does not exist
            Directory.CreateDirectory(Path.Combine(Project.Folder, subfolder));

            if (string.IsNullOrEmpty(fileName))
            {
                Console.WriteLine($"Could not generate {templateName}, name is missing");
                return;
            }

            fileName = Path.Combine(subfolder, fileName + ".Generated.cs");

            K loader = Activator.CreateInstance<K>();
            var context = new TemplateContext { TemplateLoader = loader };
            context.PushGlobal(scriptObject);
            context.IndentWithInclude = true;

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
                Console.Error.WriteLine($"Could not create code for {fileName}: {ex.InnerException?.Message ?? ex.Message ?? ""}");
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
                        return t is null ? null : _typeResolver.GetTypeString(t).ToString();
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
                    ResolvedType? resolvedType = _typeResolver.Resolve(t);
                    return resolvedType.GetTypeString();
                })
            );
            scriptObject.Import("resolve_field",
                new Func<IType, string>((t) =>
                {
                    ResolvedType? resolvedType = _typeResolver.Resolve(t);
                    return resolvedType.GetFieldString();
                })
            );

            scriptObject.Import("generate_comments",
                new Func<bool>(() => GenerateComments)
            );

            return scriptObject;
        }

        #endregion
    }
}
