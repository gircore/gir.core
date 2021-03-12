using System.Collections.Generic;
using System.Linq;
using Repository.Services;

namespace Repository
{
    internal class RepositoryInternal
    {
        private readonly LoaderService _loaderService;
        private readonly TypeReferenceResolverService _typeReferenceResolverService;

        public RepositoryInternal(LoaderService loaderService, TypeReferenceResolverService typeReferenceResolverService)
        {
            _loaderService = loaderService;
            _typeReferenceResolverService = typeReferenceResolverService;
        }

        public IEnumerable<LoadedProject> Load(ResolveFileFunc fileFunc, IEnumerable<string> targets)
        {
            Log.Information($"Initialising repository with {targets.Count()} toplevel project(s)");

            var enumerableLoadedProjects = _loaderService.LoadOrdered(targets, fileFunc);
            var loadedProjects = enumerableLoadedProjects as List<LoadedProject> ?? enumerableLoadedProjects.ToList();

            _typeReferenceResolverService.Resolve(loadedProjects);
            StripProjects(loadedProjects);

            Log.Information($"Repository initialised with {loadedProjects.Count} top-level project(s) and {loadedProjects.Count - targets.Count()} dependencies.");
            
            return loadedProjects;
        }

        private static void StripProjects(List<LoadedProject> loadedProjects)
        {
            foreach (var proj in loadedProjects)
                proj.Namespace.Strip();
        }
    }
}
