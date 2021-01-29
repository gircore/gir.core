using System;
using System.IO;
using System.Linq;
using Generator;
using XmlDocMarkdown.Core;

namespace Build
{
    public class Docs : ITarget
    {
        private readonly Settings _settings;

        public Docs(Settings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public void Execute()
        {
            foreach ((Project project, Type _) in Projects.LibraryProjects)
            {
                var settings = new XmlDocMarkdownSettings()
                {
                    VisibilityLevel = XmlDocVisibilityLevel.Public,
                    ShouldClean = true,
                    GenerateToc = true
                };

                XmlDocMarkdownResult result = XmlDocMarkdownGenerator.Generate(
                    inputPath: GetAssemblyFile(project.Folder),
                    outputPath: "../Docs/Api",
                    settings: settings
                );

                foreach (var message in result.Messages)
                    Console.WriteLine(message);
            }
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
