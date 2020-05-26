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

        public Generator(string girFile, string outputDir)
        {
            this.girFile = girFile ?? throw new System.ArgumentNullException(nameof(girFile));
            this.outputDir = outputDir ?? throw new System.ArgumentNullException(nameof(outputDir));

            var reader = new GirReader();
            repository = reader.ReadRepository(girFile);

            Directory.CreateDirectory(outputDir);
        }

        public void Generate()
        {
            if(repository.Namespace is null)
            {
                Console.WriteLine($"Could not create classes for {girFile}. Namespace is missing.");
                return;
            }

            if(repository.Namespace.Name is null)
            {
                Console.WriteLine($"Could not create classes for {girFile}. Namespace is missing a name.");
                return;
            }

            foreach(var cls in repository.Namespace.Classes)
                if(cls.Name is {})
                    Generate("../Generator/Templates/class.sbntxt", cls.Name, repository.Namespace.Name, cls);
                else
                    Console.WriteLine("Could not generate class, name is missing");
        }

        private void Generate(string templateFile, string fileName, string ns, object obj)
        {
            var scriptObject = new ScriptObject();
            scriptObject.Import(obj);
            scriptObject.Add("namespace", ns);

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