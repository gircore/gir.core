using System;
using Generator;

namespace Build
{
    public interface ILibraryBuilder
    {
        void BuildLibraries();
    }

    public class LibraryBuilder : ILibraryBuilder
    {
        private readonly Settings _settings;

        public LibraryBuilder(Settings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public void BuildLibraries()
        {
            foreach ((Project project, Type _) in Projects.LibraryProjects)
                DotNet.Build(project.Folder, _settings.Configuration);
        }
    }
}
