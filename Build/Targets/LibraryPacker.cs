using System;
using Generator;

namespace Build
{
    public interface ILibraryPacker
    {
        void PackLibraries();
    }

    public class LibraryPacker : ILibraryPacker
    {
        private readonly Settings _settings;

        public LibraryPacker(Settings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public void PackLibraries()
        {
            foreach ((Project project, Type _) in Projects.LibraryProjects)
            {
                DotNet.Pack(
                    project: project.Folder,
                    configuration: _settings.Configuration,
                    version: _settings.Version?.ToNormalizedString()
                );
            }
        }
    }
}
