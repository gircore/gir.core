using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Gir;
using Scriban;
using Scriban.Runtime;

namespace Generator
{
    public class CoreGenerator : Generator
    {
        private readonly TypeResolver typeResolver;
        private readonly string dllImport;
        
        public CoreGenerator(Project project) : base(project)
        {
            dllImport = GetDllImport(project) ?? throw new ArgumentNullException(nameof(dllImport));
            var aliases = new List<GAlias>();
            aliases.AddRange(Repository.Namespace?.Aliases ?? Enumerable.Empty<GAlias>());
            var aliasResolver = new AliasResolver(aliases);
            typeResolver = new TypeResolver(aliasResolver);
        }
        
        // Determines the dll name from the shared library (based on msys2 gtk binaries)
        // SEE: https://tldp.org/HOWTO/Program-Library-HOWTO/shared-libraries.html
        private static string GetDllImport(Project project)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return project.GetWindowsDllImport();
            else
                return project.GetLinuxDllImport();
        }

        protected override void Generate(GNamespace gNamespace)
        {
            if (gNamespace.Name is null)
                throw new Exception("Could not create code. Namespace is missing a name.");
            
            GenerateClasses(gNamespace.Classes, gNamespace.Name);
            GenerateRecords(gNamespace.Records, gNamespace.Name);
            GenerateRecords(gNamespace.Unions, gNamespace.Name);
        }

        private void GenerateRecords(IEnumerable<GRecord> records, string ns)
        {
            foreach(var record in records)
                record.Methods.InsertRange(0, record.Functions);

            GenerateClasses(records, ns, "struct", "Structs");
        }
        
        private void GenerateClasses(IEnumerable<GInterface> classes, string @namespace, string templateName = "class", string subfolder = "Classes")
        {
            foreach (var cls in classes)
            {
                var scriptObject = CreateScriptObject(@namespace, cls);
                Create(templateName, Path.Combine(subfolder, cls.Name + ".Generated.cs"), scriptObject);
            }
        }

        private ScriptObject CreateScriptObject(string @namespace, Object obj)
        {
            var scriptObject = new ScriptObject
            {
                {"namespace", @namespace},
                {"dll_import", dllImport}
            };
            scriptObject.Import(obj);
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
                    return resolvedType.Attribute + resolvedType.Type;
                })
            );
            return scriptObject;
        }

        private void Create(string templateFile, string fileName, ScriptObject scriptObject)
        {
            var context = new TemplateContext {TemplateLoader = new CoreTemplateLoader()};
            context.PushGlobal(scriptObject);
            
            templateFile = $"../Generator/Templates/Core/{templateFile}.sbntxt";
            
            
            if (fileName.Contains("Accessible"))
            {
                //TODO: Workaround for missing ATK!
                Console.WriteLine($"Skipping file {fileName} because it looks like an ATK class which is not supported.");
                return;   
            }

            GenerateCode(templateFile, fileName, context);
        }
    }
}
