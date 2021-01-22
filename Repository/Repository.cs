using System.Collections.Generic;
using System.IO;

namespace Repository
{
    public delegate FileInfo ResolveFileFunc(string name, string version);

    public record Target
    {
        public string Name { get; }
        public string Version { get; }

        public Target(string name, string version)
        {
            Name = name;
            Version = version;
        }
    }

    public class Repository
    {
        private readonly Loader _loader;
        private readonly List<LoadedProject> LoadedProjects = new();
        
        public Repository(ResolveFileFunc fileFunc, Target[] targets)
        {
            Log.Information($"Initialising generator with {targets.Length} toplevel project(s)");
            
            // Loading - Serialise all gir files into the LoadedProjects
            // dictionary. We attempt to continue regardless of whether all
            // files are loaded successfully.
            var loader = new Loader(targets, fileFunc, "../gir-files");
            
            // Sorts dependencies in order of base -> top level. Will throw if
            // a circular dependency is found.
            LoadedProjects = loader.GetOrderedList();
            
            // Resolve References
            // The Parser creates unresolved references. We need to crawl through
            // project data and resolve each reference relative to the current
            // namespace. A TypeDictionaryView allows us to query qualified
            // (e.g Gtk.Application) and unqualified (e.g. Application) names
            // from the perspective of a given namespace and have them resolve properly.

            foreach (LoadedProject proj in LoadedProjects)
            {
                // var unresolved = new List<UnresolvedReference>();
                
                // Add Symbols
                // Catalog References
                
                // Resolve References
            }
        }
    }
}
