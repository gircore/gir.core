using System.Collections.Generic;
using System.IO;

namespace Repository
{
    public delegate FileInfo ResolveFileFunc(string nspace, string version);
    
    public class Repository
    {
        private readonly Loader _loader;
        private readonly List<LoadedProject> LoadedProjects = new();
        
        public Repository(ResolveFileFunc fileFunc, string[] targets)
        {
            Log.Information($"Initialising generator with {targets.Length} toplevel project(s)");
            
            // Loading - Serialise all gir files into the LoadedProjects
            // dictionary. We attempt to continue regardless of whether all
            // files are loaded successfully.
            var loader = new Loader(targets, "../gir-files");
            
            // Sorts dependencies in order of base -> top level. Will throw if
            // a circular dependency is found.
            LoadedProjects = loader.GetOrderedList();
        }
    }
}
