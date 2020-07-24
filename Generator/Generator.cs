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
    public class Generator
    {
        private readonly string girFile;
        private readonly string outputDir;
        private GRepository repository;
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
                string name = components[0];
                string version = components[1];

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

        public Generator(string girFile, string outputDir, string dllImport, IEnumerable<string> aliasFiles)
        {
            this.dllImport = ConvertLibName(dllImport) ?? throw new ArgumentNullException(nameof(dllImport));
            this.girFile = girFile ?? throw new System.ArgumentNullException(nameof(girFile));
            this.outputDir = outputDir ?? throw new System.ArgumentNullException(nameof(outputDir));

            var reader = new GirReader();
            repository = reader.ReadRepository(girFile);

            var aliases = new List<GAlias>();
            aliases.AddRange(repository?.Namespace?.Aliases ?? Enumerable.Empty<GAlias>());
            foreach(var aliasGir in aliasFiles)
            {
                var repo = reader.ReadRepository(aliasGir);
                aliases.AddRange(repo.Namespace?.Aliases ?? Enumerable.Empty<GAlias>());
            }

            var aliasResolver = new AliasResolver(aliases);
            typeResolver = new TypeResolver(aliasResolver);

            Directory.CreateDirectory(outputDir);
        }

        public void Generate()
        {
            if (repository.Namespace is null)
            {
                Console.WriteLine($"Could not create classes for {girFile}. Namespace is missing.");
                return;
            }

            if (repository.Namespace.Name is null)
            {
                Console.WriteLine($"Could not create classes for {girFile}. Namespace is missing a name.");
                return;
            }

            var ns = repository.Namespace.Name;
            GenerateClasses(repository.Namespace.Classes, ns);
            GenerateInterfaces(repository.Namespace.Interfaces, ns);
            GenerateRecords(repository.Namespace.Records, ns);
            GenerateRecords(repository.Namespace.Unions, ns);
            GenerateEnums(repository.Namespace.Bitfields, ns, true);
            GenerateEnums(repository.Namespace.Enumerations, ns, false);
            GenerateDelegates(repository.Namespace.Callbacks, ns);
            GenerateMethods(repository.Namespace.Functions, ns);
            GenerateConstants(repository.Namespace.Constants, ns);
        }

        private void GenerateConstants(IEnumerable<GConstant> constants, string ns)
        {
            var scriptObject = new ScriptObject();
            scriptObject.Add("constants", constants);
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
            var scriptObject = new ScriptObject();
            scriptObject.Add("delegates", delegates);
            Generate("delegates", "Delegates", ns, scriptObject);
        }

        private void GenerateMethods(List<GMethod> methods, string ns)
        {
            var scriptObject = new ScriptObject();
            RemoveVarArgsMethods(methods);
            scriptObject.Add("methods", methods);
            Generate("methods", "Methods", ns, scriptObject);
        }

        private void GenerateEnums(IEnumerable<GEnumeration> enums, string ns, bool hasFlags)
        {
            foreach (var e in enums)
            {
                var scriptObject = new ScriptObject();
                scriptObject.Add("has_flags", hasFlags);

                if (e.Name is { })
                    Generate("enum", e.Name, ns, scriptObject, e);
                else
                    Console.WriteLine("Could not generate enum, name is missing");
            }
        }

        private void RemoveVarArgsMethods(List<GMethod> methods)
        {
            Func<GParameter, bool> isVariadic = (p) => p.VarArgs is {};
            methods.RemoveAll((x) => x.Parameters?.Parameters.Any(isVariadic) ?? false);
        }

        private void Generate(string templateFile, string fileName, string ns, object? obj = null)
            => Generate(templateFile, fileName, ns, new ScriptObject(), obj);

        private void Generate(string templateFile, string fileName, string ns, ScriptObject scriptObject, object? obj = null)
        {
            templateFile = $"../Generator/Templates/{templateFile}.sbntxt";
            var resolveType = new Func<IType, string>((t) => typeResolver.Resolve(t));
            var commentLineByLine = new Func<string, string>((s) => s.CommentLineByLine());
            var makeSingleLine = new Func<string, string>((s) => s.MakeSingleLine());
            var escapeQuotes = new Func<string, string>((s) => s.EscapeQuotes());
            var fixIdentifier = new Func<string, string>((s) => s.FixIdentifier());
            var debug = new Action<string>((s) => Console.WriteLine(s));
            var getType = new Func<GType, string>((t) => typeResolver.GetTypeString(t));

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

            scriptObject.Add("namespace", ns);
            scriptObject.Add("dll_import", dllImport);

            var context = new TemplateContext();
            context.TemplateLoader = new TemplateLoader();
            context.PushGlobal(scriptObject);

            try
            {
                var template = Template.Parse(File.ReadAllText(templateFile));
                var content = template.Render(context);
                Write(fileName, content);
            }
            catch(Exception ex)
            {
                Console.Error.WriteLine($"Could not create Wrapper for {fileName}: {ex.Message}");
            }
        }

        private void Write(string name, string content)
        {
            var fileName = name + ".cs";
            var path = Path.Combine(outputDir, fileName);

            File.WriteAllText(path, content);
        }
    }
}