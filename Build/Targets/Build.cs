using System;

namespace Build
{
    public class Build : ExecuteableTarget
    {
        private readonly Settings _settings;

        public string Description => "Builds the project.";

        public string[] DependsOn => new [] { nameof(Generate) };
        
        public Build(Settings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public void Execute()
        {
            foreach (Project proj in Projects.AllLibraries)
            {
                DotNet.Build(
                    project: proj.Folder,
                    configuration: _settings.Configuration,
                    version: _settings.Version?.ToNormalizedString()
                );
            }
        }
    }
}
