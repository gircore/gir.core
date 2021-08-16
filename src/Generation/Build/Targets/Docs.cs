using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using XmlDocMarkdown.Core;

namespace Build
{
    public class Docs : ExecuteableTarget
    {
        private readonly Settings _settings;

        public string Description => "Generate API documentation.";
        public string[] DependsOn => new[] { nameof(Build) };

        public Docs(Settings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public void Execute()
        {
            if (_settings.GenerateAsynchronously)
            {
                Parallel.ForEach(Projects.AllLibraries, CreateMarkdown);
            }
            else
            {
                foreach (Project project in Projects.AllLibraries)
                    CreateMarkdown(project);
            }
        }

        private void CreateMarkdown(Project project)
        {
            var settings = new XmlDocMarkdownSettings()
            {
                VisibilityLevel = XmlDocVisibilityLevel.Public,
                ShouldClean = true,
                GenerateToc = true
            };

            XmlDocMarkdownResult result = XmlDocMarkdownGenerator.Generate(
                inputPath: GetAssemblyFile(project.Folder),
                outputPath: "../../../docs/Api",
                settings: settings
            );

            foreach (var message in result.Messages)
                Console.WriteLine(message);
        }

        private string GetAssemblyFile(string folder)
        {
            var info = new DirectoryInfo(folder);
            var dllName = $"{info.Name}.dll";
            var files = Directory.GetFiles($"{folder}/bin/", dllName, SearchOption.AllDirectories);
            return files.First();
        }
    }
}
