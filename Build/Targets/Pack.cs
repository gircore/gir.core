using System;
using Repository.Xml;

namespace Build
{
    public class Pack : ITarget
    {
        private readonly Settings _settings;

        public Pack(Settings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public void Execute()
        {
            foreach ((Project project, Type _) in Projects.LibraryProjects)
            {
                DotNet.Pack(
                    project: project.Folder,
                    configuration: _settings.Configuration,
                    version: _settings.Version?.ToNormalizedString(),
                    outputDir: "../Nuget"
                );
            }
        }
    }
}
