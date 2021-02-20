using System;
using System.IO;
using System.Linq;

namespace Build
{
    public class Clean : ITarget
    {
        private readonly Settings _settings;

        public Clean(Settings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public void Execute()
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
            foreach (Project project in Projects.AllLibraries)
            {
                CleanUp(project.Folder, _settings.Configuration);
            }
        }

        private static void CleanUp(string project, Configuration configuration)
        {
            if (Directory.Exists(project))
            {
                foreach (var d in Directory.EnumerateDirectories(project))
                {
                    foreach (var file in Directory.EnumerateFiles(d).Where(FileIsGenerated))
                    {
                        File.Delete(file);
                    }
                }
            }

            DotNet.Clean(project, configuration);
        }

        private static bool FileIsGenerated(string file) => file.Contains(".Generated.");
    }
}
