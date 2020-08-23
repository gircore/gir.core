using System;
using System.IO;
using System.Xml.Serialization;
using Gir;
using Scriban;

namespace Generator
{
    public abstract class Generator
    {
        private readonly string girFile;
        private readonly string outputDir;
        
        #region Properties
        private readonly GRepository repository;
        protected GRepository Repository => repository;
        #endregion Properties
        
        protected Generator(string girFile, string outputDir)
        {
            this.girFile = girFile ?? throw new ArgumentNullException(nameof(girFile));
            this.outputDir = outputDir ?? throw new ArgumentNullException(nameof(outputDir));
            
            repository = ReadRepository(girFile);

            Directory.CreateDirectory(outputDir);
        }

        public void Generate()
        {
            if (repository.Namespace is null)
                throw new Exception($"Can not generate for {girFile}. Namespace is missing.");

            Generate(repository.Namespace);
        }
        
        protected static GRepository ReadRepository(string girFile)
        {
            var serializer = new XmlSerializer(typeof(GRepository), "http://www.gtk.org/introspection/core/1.0");

            using var fs = new FileStream(girFile, FileMode.Open);
            return (GRepository) serializer.Deserialize(fs);
        }

        protected abstract void Generate(GNamespace gNamespace);

        protected void GenerateCode(string templateFile, string fileName, TemplateContext context)
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
            var path = Path.Combine(outputDir, fileName);
            File.WriteAllText(path, content);
        }
    }
}