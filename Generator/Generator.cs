using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Introspection;

namespace Generator
{
    public class Generator
    {
        public readonly Dictionary<Project, GRepository> LoadedProjects = default;
        
        public Generator(Project[] projects)
        {
            Log.Information($"Initialising generator with {projects.Length} toplevel project(s)");
            
            // Loading - Serialise all gir files into the LoadedProjects
            // dictionary. We attempt to continue regardless of whether all
            // files are loaded successfully.
            LoadProjects(projects);
            
            // Processing - We need to transform raw gir data to create a more
            // ergonomic C# API. This includes prefixing interfaces, storing rich
            // type information, etc
            
            // 1. Order dependencies
            
            // 2. Process symbols (Add delegate to hook into symbol processing)
            
            // Validation - Ensure all symbols are resolved and that the
            // solution will (theoretically) compile. Report errors to the user

        }

        private void LoadProjects(Project[] projects)
        {
            foreach (Project proj in projects)
            {
                try
                {
                    GRepository repo = Loader.SerializeGirFile(proj.Gir);
                    LoadedProjects.Add(proj, repo);
                }
                catch (Exception e)
                {
                    // Log and attempt to continue
                    Log.Error($"Failed to load gir file {proj.Gir}", e.Message);
                }
            }
        }

        public async Task<int> WriteAsync()
        {
            // foreach...
            return 0;
        }
    }
}
