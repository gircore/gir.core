using System;
using System.Collections.Generic;
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
        
        public CoreGenerator(string girFile, string outputDir, string dllImport) : base(girFile, outputDir)
        {
            this.dllImport = ConvertLibName(dllImport) ?? throw new ArgumentNullException(nameof(dllImport));
            var aliases = new List<GAlias>();
            aliases.AddRange(Repository.Namespace?.Aliases ?? Enumerable.Empty<GAlias>());
            var aliasResolver = new AliasResolver(aliases);
            typeResolver = new TypeResolver(aliasResolver);
        }
        
        // Determines the dll name from the shared library (based on msys2 gtk binaries)
        // SEE: https://tldp.org/HOWTO/Program-Library-HOWTO/shared-libraries.html
        private static string ConvertLibName(string sharedLibrary)
        {
            var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

            if (!isWindows)
                return sharedLibrary;

            string dllName;
            
            if (sharedLibrary.Contains(".so."))
            {
                // We have a version number at the end
                // e.g. libcairo-gobject.so.2
                string[] components = sharedLibrary.Split(".so.");
                var name = components[0];
                var version = components[1];

                dllName = $"{name}-{version}.dll";
            }
            else
            {
                // There is no version number at the end
                // Simply add ".dll"
                string name = sharedLibrary.Split(".so")[0];
                dllName = $"{name}.dll";
            }

            Console.WriteLine($"Renaming {sharedLibrary} to {dllName}");
            return dllName;
        }

        protected override void Generate(GNamespace gNamespace)
        {
            if (gNamespace.Name is null)
                throw new Exception("Could not create code. Namespace is missing a name.");
            
            GenerateClasses(gNamespace.Classes, gNamespace.Name);
        }

        private void GenerateClasses(IEnumerable<GInterface> classes, string @namespace)
        {
            foreach (var cls in classes)
            {
                var scriptObject = new ScriptObject
                {
                    {"namespace", @namespace},
                    {"dll_import", dllImport}
                };
                scriptObject.Import(cls);
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

                Create("class", cls.Name + ".Generated.cs", scriptObject);
            }
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
