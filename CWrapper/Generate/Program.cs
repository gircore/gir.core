using System.IO;
using Mono.TextTemplating;

namespace Template
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateTemplate("Class");
            CreateTemplate("Enum");
            CreateTemplate("Struct");
            CreateTemplate("Delegates");
            CreateTemplate("Methods");
        }

        private static void CreateTemplate(string type)
        {
            string template = File.ReadAllText($"../Templates/{type}Template.tt");
            
            var generator = new TemplateGenerator();
            generator.PreprocessTemplate($"../Templates/{type}Template.tt", $"{type}Generator", "CWrapper", template, out var language, out var references, out var content);

            File.WriteAllText($"../{type}Generator.Generated.cs", content);
        }
    }
}
