using System;
using System.IO;
using System.Xml.Serialization;
using Gir;
using Scriban;

namespace Generator
{
    public abstract class Generator
    {
        #region Properties
        protected GRepository Repository { get; }
        protected Project Project { get; }
        #endregion Properties
        
        protected Generator(Project project)
        {
            Project = project ?? throw new ArgumentNullException(nameof(project));
            
            Repository = ReadRepository(project.Gir);
        }

        public void Generate()
        {
            if (Repository.Namespace is null)
                throw new Exception($"Can not generate for {Project.Name}. Namespace is missing.");

            Generate(Repository.Namespace);
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
            var path = Path.Combine(Project.Folder, fileName);
            File.WriteAllText(path, content);
        }
    }
}