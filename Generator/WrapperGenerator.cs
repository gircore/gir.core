using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Gir;
using Scriban;
using Scriban.Runtime;

namespace Generator
{
    public class WrapperGenerator : Generator
    {
        private readonly string dllImport;
        private readonly TypeResolver typeResolver;

        // Determines the dll name from the shared library (based on msys2 gtk binaries)
        // SEE: https://tldp.org/HOWTO/Program-Library-HOWTO/shared-libraries.html
        private string ConvertLibName(string sharedLibrary)
        {
            bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

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

        public WrapperGenerator(string girFile, string outputDir, string dllImport, IEnumerable<string> aliasFiles) : base(girFile, outputDir)
        {
            this.dllImport = ConvertLibName(dllImport) ?? throw new ArgumentNullException(nameof(dllImport));

            var aliases = new List<GAlias>();
            aliases.AddRange(Repository.Namespace?.Aliases ?? Enumerable.Empty<GAlias>());
            foreach(var aliasGir in aliasFiles)
            {
                var repo = ReadRepository(aliasGir);
                aliases.AddRange(repo.Namespace?.Aliases ?? Enumerable.Empty<GAlias>());
            }

            var aliasResolver = new AliasResolver(aliases);
            typeResolver = new TypeResolver(aliasResolver);
        }

        protected override void Generate(GNamespace gNamespace)
        {
            if (gNamespace.Name is null)
                throw new Exception("Could not create code. Namespace is missing a name.");
            
            var ns = gNamespace.Name;
            GenerateClasses(gNamespace.Classes, ns);
            GenerateInterfaces(gNamespace.Interfaces, ns);
            GenerateRecords(gNamespace.Records, ns);
            GenerateRecords(gNamespace.Unions, ns);
            GenerateEnums(gNamespace.Bitfields, ns, true);
            GenerateEnums(gNamespace.Enumerations, ns, false);
            GenerateDelegates(gNamespace.Callbacks, ns);
            GenerateMethods(gNamespace.Functions, ns);
            GenerateConstants(gNamespace.Constants, ns);
        }

        private void GenerateConstants(IEnumerable<GConstant> constants, string ns)
        {
            var scriptObject = new ScriptObject
            {
                {"constants", constants}
            };
            Generate("constants", "Constants", ns, scriptObject);
        }

        private void GenerateRecords(IEnumerable<GRecord> records, string ns)
        {
            foreach(var record in records)
                record.Methods.InsertRange(0, record.Functions);

            GenerateClasses(records, ns, "struct");
        }

        private void GenerateClasses(IEnumerable<GClass> classes, string ns, string template = "class")
        {
            foreach (var cls in classes)
                cls.Methods.InsertRange(0, cls.Constructors);

            GenerateInterfaces(classes, ns, template);
        }

        private void GenerateInterfaces(IEnumerable<GInterface> classes, string ns, string template = "class")
        {
            foreach (var cls in classes)
            {
                RemoveVarArgsMethods(cls.Methods);
                
                if (cls.Name is { })
                    Generate(template, cls.Name, ns, cls);
                else
                    Console.WriteLine("Could not generate class, name is missing");
            }
        }

        private void GenerateDelegates(IEnumerable<GCallback> delegates, string ns)
        {
            var scriptObject = new ScriptObject {{"delegates", delegates}};
            Generate("delegates", "Delegates", ns, scriptObject);
        }

        private void GenerateMethods(List<GMethod> methods, string ns)
        {
            RemoveVarArgsMethods(methods);

            var scriptObject = new ScriptObject {{"methods", methods}};
            Generate("methods", "Methods", ns, scriptObject);
        }

        private void GenerateEnums(IEnumerable<GEnumeration> enums, string ns, bool hasFlags)
        {
            foreach (var e in enums)
            {
                var scriptObject = new ScriptObject {{"has_flags", hasFlags}};

                if (e.Name is { })
                    Generate("enum", e.Name, ns, scriptObject, e);
                else
                    Console.WriteLine("Could not generate enum, name is missing");
            }
        }

        private void RemoveVarArgsMethods(List<GMethod> methods)
        {
            static bool IsVariadic(GParameter p) => p.VarArgs is {};
            methods.RemoveAll((x) => x.Parameters?.Parameters.Any(IsVariadic) ?? false);
        }

        private void Generate(string templateFile, string fileName, string ns, object? obj = null)
            => Generate(templateFile, fileName, ns, new ScriptObject(), obj);

        private void Generate(string templateFile, string fileName, string ns, ScriptObject scriptObject, object? obj = null)
        {
            var subnamespace = "Sys";
            templateFile = $"../Generator/Templates/Wrapper/{templateFile}.sbntxt";
            var resolveType = new Func<IType, string>((t) =>
            {
                var resolvedType =  typeResolver.Resolve(t);
                return resolvedType.Attribute + resolvedType.Type.Replace(".", $".{subnamespace}.");
            });
            var commentLineByLine = new Func<string, string>((s) => s.CommentLineByLine());
            var makeSingleLine = new Func<string, string>((s) => s.MakeSingleLine());
            var escapeQuotes = new Func<string, string>((s) => s.EscapeQuotes());
            var fixIdentifier = new Func<string, string>((s) => s.FixIdentifier());
            var debug = new Action<string>(Console.WriteLine);
            var getType = new Func<GType, string>((t) => typeResolver.GetTypeString(t).ToString());

            if(obj is {})
            {
                scriptObject.Import(obj);
            }
            scriptObject.Import("comment_line_by_line", commentLineByLine);
            scriptObject.Import("make_single_line", makeSingleLine);
            scriptObject.Import("escape_quotes", escapeQuotes);
            scriptObject.Import("fix_identifier", fixIdentifier);
            scriptObject.Import("resolve_type", resolveType);
            scriptObject.Import("debug", debug);
            scriptObject.Import("type_to_string", getType);
            
            scriptObject.Add("namespace", $"{ns}.{subnamespace}");
            scriptObject.Add("dll_import", dllImport);

            var context = new TemplateContext {TemplateLoader = new WrapperTemplateLoader()};
            context.PushGlobal(scriptObject);

            GenerateCode(templateFile, fileName + ".cs", context);
        }
    }
}
