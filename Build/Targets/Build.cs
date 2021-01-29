using System;
using Generator;

namespace Build
{
    public class Build : ITarget
    {
        private readonly Settings _settings;

        public Build(Settings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public void Execute()
        {
            foreach ((Project project, Type _) in Projects.LibraryProjects)
            {
                DotNet.Build(
                    project: project.Folder,
                    configuration: _settings.Configuration,
                    version: _settings.Version?.ToNormalizedString()
                );
            }
        }
    }
}
