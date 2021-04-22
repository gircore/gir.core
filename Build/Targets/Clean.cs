using System;
using System.IO;
using System.Linq;

namespace Build
{
    public class Clean : ExecuteableTarget
    {
        private readonly Settings _settings;

        public string Description => "Cleans samples and build output including generated source code files.";

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
            try
            {
                CleanUpInternal(project, configuration);
            }
            catch (Exception ex)
            {
                Log.Error($"Could not clean {project} ({configuration}): {ex.Message}");
            }
        }

        private static void CleanUpInternal(string project, Configuration configuration)
        {
            if (Directory.Exists(project))
            {
                foreach (var d in Directory.EnumerateDirectories(project, "*", SearchOption.AllDirectories))
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
