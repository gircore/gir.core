using System;

namespace Build
{
    public class Pack : ExecuteableTarget
    {
        private readonly Settings _settings;

        public string Description => "Packs the libraries into the 'Nuget' folder in the project root.";
        public string[] DependsOn => new[] { nameof(Build) };

        public Pack(Settings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public void Execute()
        {
            foreach (Project project in Projects.AllLibraries)
            {
                DotNet.Pack(
                    project: project.Folder,
                    configuration: _settings.Configuration,
                    version: _settings.Version?.ToNormalizedString(),
                    outputDir: "../../../Nuget"
                );
            }
        }
    }
}
