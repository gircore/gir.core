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

        public IEnumerable<LoadedProject> Load(ResolveFileFunc fileFunc, string[] targets)
        {
            Log.Information($"Initialising generator with {targets.Length} toplevel project(s)");

            var enumerableLoadedProjects = _loaderService.LoadOrdered(targets, fileFunc);
            var loadedProjects = enumerableLoadedProjects as List<LoadedProject> ?? enumerableLoadedProjects.ToList();
            
            _typeReferenceResolverService.Resolve(loadedProjects);
            
            Log.Information($"Repository initialised with {loadedProjects.Count} top-level project(s) and {loadedProjects.Count - targets.Length} dependencies.");
            
            return loadedProjects;
        }
    }
}
