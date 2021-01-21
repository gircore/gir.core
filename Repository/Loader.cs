using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Repository.Graph;
using Repository.Model;

namespace Repository
{
    public record LoadedProject : INode
    {
        List<INode> INode.Dependencies { get; } = new();
        string INode.Name => ProjectName;
        
        public string ProjectName { get; }
        public Namespace Namespace { get; }

        public LoadedProject(string name, Namespace nspace, IEnumerable<LoadedProject> dependencies)
        {
            ProjectName = name;
            Namespace = nspace;
            (this as INode).Dependencies.AddRange(dependencies);
        }
    }
    
    public class Loader
    {
        private readonly List<LoadedProject> _loadedProjects;
        private readonly Resolver _resolver;
        
        private readonly ResolveFileFunc _lookupFunc;

        // Set to true to signify a library has failed to load
        private bool failureFlag = false;

        // Where 'projects' are toplevel projects. Loader resolves
        // dependent gir libraries automatically.
        public Loader(string[] targets, string prefix)
        {
            _loadedProjects = new List<LoadedProject>();
            
            foreach (var target in targets)
                LoadRecursive(target);
            
            if (failureFlag)
                Log.Warning($"Failed to load some projects. Please check the log for more information.");

            _resolver = new Resolver(_loadedProjects);
        }

        public List<LoadedProject> GetOrderedList()
            => _resolver.GetOrderedList()
                .Cast<LoadedProject>()
                .ToList();

        private void LoadRecursive(string target)
            => LoadRecursive(new FileInfo(target), out LoadedProject loadedProj);

        private bool LoadRecursive(FileInfo target, [NotNullWhen(true)] out LoadedProject loadedProj)
        {
            loadedProj = null;
            
            try
            {
                // Serialize introspection data (xml)
                var parser = new Parser(target);
                var nspace = parser.Parse();

                var projName = $"{nspace.Name}-{nspace.Version}";

                // Load dependencies recursively
                var dependencies = new List<LoadedProject>();
                foreach (var (name, version) in parser.GetDependencies())
                {
                    // Skip if already loaded
                    var canonicalName = $"{name}-{version}";
                    if (_loadedProjects.Any(lp => lp.ProjectName == canonicalName))
                        continue;
                    
                    // Attempt to resolve file
                    FileInfo dependency = _lookupFunc(name, version);

                    // Load recursively
                    var result = LoadRecursive(dependency, out LoadedProject loadedDep);
                    dependencies.Add(loadedDep);

                    if (!result)
                    {
                        Log.Error($"Could not resolve '{canonicalName}' (dependency of '{projName}')");
                        failureFlag = true;
                        return false;
                    }
                }
                
                loadedProj = new LoadedProject(projName, nspace, dependencies);
                _loadedProjects.Add(loadedProj);
                
                Log.Information($"Loaded '{projName}' (provided by '{target.Name}')");
                
                return true;
            }
            catch (Exception e)
            {
                // Log and attempt to continue
                Log.Error($"Failed to load gir file '{target.Name}'.");
                Log.Exception(e);
                failureFlag = true;
                return false;
            }
        }
    }
}
