using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

using Generator.Graph;
using Generator.Introspection;

namespace Generator
{
    public record LoadedProject : INode
    {
        List<INode> INode.Dependencies { get; } = new();
        string INode.Name => ProjectData.Gir;
        
        public Project ProjectData { get; }
        public GRepository Repository { get; }

        public LoadedProject(Project data, GRepository repo, IEnumerable<LoadedProject> dependencies)
        {
            ProjectData = data;
            Repository = repo;
            (this as INode).Dependencies.AddRange(dependencies);
        }
    }
    
    public class Loader
    {
        private readonly List<LoadedProject> _loadedProjects;
        private readonly Resolver _resolver;
        private readonly string _prefix;

        // Set to true to signify a library has failed to load
        private bool failureFlag = false;

        // Where 'projects' are toplevel projects. Loader resolves
        // dependent gir libraries automatically.
        public Loader(Project[] projects, string prefix)
        {
            _prefix = prefix;
            _loadedProjects = new List<LoadedProject>();
            
            foreach (Project proj in projects)
                LoadProjectRecursive(proj);
            
            if (failureFlag)
                Log.Warning($"Failed to load some projects. Please check the log for more information.");

            _resolver = new Resolver(_loadedProjects);
        }

        public IEnumerable<LoadedProject> GetOrderedList()
            => _resolver.GetOrderedList().Cast<LoadedProject>();

        private void LoadProjectRecursive(Project proj)
            => LoadProjectRecursive(proj, out LoadedProject loadedProj);

        private bool LoadProjectRecursive(Project proj, [NotNullWhen(true)] out LoadedProject loadedProj)
        {
            loadedProj = null;
            
            try
            {
                // Serialize introspection data (xml)
                GRepository repo = SerializeGirFile(proj.Gir);
                
                // Load dependencies recursively
                List<LoadedProject> dependencies = new();
                foreach (GInclude include in repo.Includes)
                {
                    // Construct filename: e.g. "Gsk-4.0.gir"
                    var filename = $"{include.Name}-{include.Version}.gir";
                    
                    // Skip if already loaded
                    if (_loadedProjects.Any(lp => lp.ProjectData.Gir == filename))
                        continue;

                    // Load recursively
                    var dep = new Project(include.Name, filename);
                    var result = LoadProjectRecursive(dep, out LoadedProject loadedDep);
                    dependencies.Add(loadedDep);

                    if (!result)
                    {
                        Log.Error($"Could not resolve '{filename}' (dependency of '{proj.Gir}')");
                        failureFlag = true;
                        return false;
                    }
                }

                loadedProj = new LoadedProject(proj, repo, dependencies);
                _loadedProjects.Add(loadedProj);
                Log.Information($"Loaded '{proj.Gir}'");
                
                return true;
            }
            catch (Exception e)
            {
                // Log and attempt to continue
                Log.Error($"Failed to load gir file '{proj.Gir}'", e.Message);
                failureFlag = true;
                return false;
            }
        }
        
        
        private GRepository SerializeGirFile(string girFile)
        {
            var serializer = new XmlSerializer(
                type: typeof(GRepository),
                defaultNamespace: "http://www.gtk.org/introspection/core/1.0");

            // First check current directory, then built-in cache
            var path = girFile;
            if (!File.Exists(path))
                path = Path.Combine(_prefix, girFile);
            if (!File.Exists(path))
                throw new FileNotFoundException($"Could not find '{girFile}' at path '{Path.GetFullPath(path)}' or in the current directory '{Directory.GetCurrentDirectory()}'.");
            
            using var fs = new FileStream(path, FileMode.Open);
            
            return (GRepository)serializer.Deserialize(fs);
        }
    }
}
