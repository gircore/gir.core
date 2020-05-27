using System;
using System.IO;
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

        public Generator(string girFile, string outputDir, string dllImport)
        {
            this.dllImport = dllImport ?? throw new ArgumentNullException(nameof(dllImport));
            this.girFile = girFile ?? throw new System.ArgumentNullException(nameof(girFile));
            this.outputDir = outputDir ?? throw new System.ArgumentNullException(nameof(outputDir));

            var reader = new GirReader();
            repository = reader.ReadRepository(girFile);

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

            foreach (var cls in repository.Namespace.Classes)
                if (cls.Name is { })
                    Generate("../Generator/Templates/class.sbntxt", cls.Name, repository.Namespace.Name, cls);
                else
                    Console.WriteLine("Could not generate class, name is missing");
        }

        private void Generate(string templateFile, string fileName, string ns, object obj)
        {
            var commentLineByLine = new Func<string, string>((string s) => s.CommentLineByLine());
            var makeSingleLine = new Func<string, string>((string s) => s.MakeSingleLine());
            var escapeQuotes = new Func<string, string>((string s) => s.EscapeQuotes());

            var scriptObject = new ScriptObject();
            scriptObject.Import(obj);
            scriptObject.Import("comment_line_by_line", commentLineByLine);
            scriptObject.Import("make_single_line", makeSingleLine);
            scriptObject.Import("escape_quotes", escapeQuotes);
            scriptObject.Add("namespace", ns);
            scriptObject.Add("dll_import", dllImport);

            var context = new TemplateContext();
            context.TemplateLoader = new TemplateLoader();
            context.PushGlobal(scriptObject);

            var template = Template.Parse(File.ReadAllText(templateFile));
            Write(fileName, template.Render(context));
        }

        private void Write(string name, string content)
        {
            var fileName = name + ".cs";
            var path = Path.Combine(outputDir, fileName);

            File.WriteAllText(path, content);
        }
    }
}