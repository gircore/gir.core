using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using Repository.Graph;
using Repository.Model;

namespace Repository
{

    internal class LoaderService
    {
        private readonly DependencyResolverService<Namespace> _dependencyResolverService;
        private readonly IncludeFactory _includeFactory;
        private readonly NamespaceFactory _namespaceFactory;
        private readonly Xml.Loader _xmlLoader;
        private ResolveFileFunc? _lookupFunc;

        private bool _projectLoadFailed;

        public LoaderService(DependencyResolverService<Namespace> dependencyResolverService, IncludeFactory includeFactory, NamespaceFactory namespaceFactory, Xml.Loader xmlLoader)
        {
            _dependencyResolverService = dependencyResolverService;
            _includeFactory = includeFactory;
            _namespaceFactory = namespaceFactory;
            _xmlLoader = xmlLoader;
        }

        // Where 'targets' are toplevel projects. LoaderService resolves
        // dependent gir libraries automatically.
        public IEnumerable<Namespace> LoadOrdered(IEnumerable<string> targets, ResolveFileFunc lookupFunc)
        {
            _lookupFunc = lookupFunc;
            var namespaces = new List<Namespace>();

            foreach (var target in targets)
                LoadAndAddProjects(namespaces, target);

            if (_projectLoadFailed)
                Log.Warning($"Failed to load some projects. Please check the log for more information.");

            return _dependencyResolverService.ResolveOrdered(namespaces).Cast<Namespace>();
        }

        private void LoadAndAddProjects(ICollection<Namespace> namespaces, string target)
        {
            var info = new FileInfo(target);
            _ = LoadRecursive(namespaces, info);
        }

        private Namespace? LoadRecursive(ICollection<Namespace> namespaces, FileInfo target)
        {
            try
            {
                var repoinfo = _xmlLoader.LoadRepository(target);

                if (repoinfo.Namespace is null)
                    throw new Exception($"Repository does not include any {nameof(repoinfo.Namespace)}.");

                var repositoryInfoData = _includeFactory.CreateFromNamespaceInfo(repoinfo.Namespace);

                if (TryLoadProject(namespaces, repositoryInfoData, out Namespace? project))
                    return project;

                var dependencies = LoadDependencies(namespaces, repoinfo.Includes);
                var nspace = _namespaceFactory.CreateFromNamespace(repoinfo.Namespace);
                nspace.SetDependencies(dependencies);
                namespaces.Add(nspace);

                Log.Information($"Loaded '{nspace.ToCanonicalName()}' (provided by '{target.Name}')");

                return project;
            }
            catch (Exception e)
            {
                Log.Error($"Failed to load gir file '{target.Name}'.");
                Log.Exception(e);
                _projectLoadFailed = true;

                return null;
            }
        }

        private IEnumerable<Namespace> LoadDependencies(ICollection<Namespace> ns, IEnumerable<Xml.Include> includes)
        {
            var dependencies = new List<Namespace>();
            foreach (var dependency in includes.Select(_includeFactory.Create))
            {
                FileInfo dependentGirFile = GetGirFileInfo(dependency);
                var dependentProject = LoadRecursive(ns, dependentGirFile);

                if (dependentProject is not null)
                    dependencies.Add(dependentProject);
            }

            return dependencies;
        }

        private bool TryLoadProject(IEnumerable<Namespace> loadedProjects, Include repo, [NotNullWhen(true)] out Namespace? loadedProject)
        {
            var foundProjects = loadedProjects.Where(x => NameMatches(x, repo)).ToArray();
            switch (foundProjects.Length)
            {
                case 0:
                    loadedProject = null;
                    return false;
                case 1:
                    loadedProject = foundProjects[0];
                    return true;
                default:
                    throw new Exception("Inconsistent data. Projects are loaded multiple times. Aborting");
            }
        }

        private bool NameMatches(Namespace ns, Include repo)
            => ns.ToCanonicalName() == repo.ToCanonicalName();

        private FileInfo GetGirFileInfo(Include data)
        {
            if (_lookupFunc is null)
                throw new Exception("Lookup func is not initialized");

            return _lookupFunc(data.Name, data.Version);
        }

       
    }
}
