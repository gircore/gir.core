using System;
using System.IO;

namespace Build
{
    public interface ICleaner
    {
        void Clean();
    }

    public class Cleaner : ICleaner
    {
        private readonly Settings _settings;

        public Cleaner(Settings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }
        
        public void Clean()
        {
            CleanSamples();
            CleanLibraries();
        }

        private void CleanSamples()
        {
            foreach (var sampleProject in Projects.SampleProjects)
            {
                CleanUp(sampleProject, _settings.Configuration);
            }
        }

        private void CleanLibraries()
        {
            foreach (var libraryProject in Projects.LibraryProjects)
            {
                CleanUp(libraryProject.Project.Folder, _settings.Configuration);
            }
        }

        private static void CleanUp(string project, Configuration configuration)
        {
            if (Directory.Exists(project))
            {
                foreach (var d in Directory.EnumerateDirectories(project))
                {
                    foreach (var file in Directory.EnumerateFiles(d))
                    {
                        if (file.Contains(".Generated."))
                            File.Delete(file);
                    }
                }
            }

            DotNet.Clean(project, configuration);
        }
    }
}
