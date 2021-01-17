using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Generator.Introspection;

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
            new Loader(projects, "../gir-files");

            // Processing - We need to transform raw gir data to create a more
            // ergonomic C# API. This includes prefixing interfaces, storing rich
            // type information, etc

            // 1. Attempt to load all dependencies

            // 2. Order dependencies

            // 3. Process symbols (Add delegate to hook into symbol processing)

            // Validation - Ensure all symbols are resolved and that the
            // solution will (theoretically) compile. Report errors to the user

        }

        public async Task<int> WriteAsync()
        {
            // foreach...
            return 0;
        }
    }
}
