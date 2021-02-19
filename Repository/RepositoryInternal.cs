using System.Collections.Generic;
using System.Linq;
using Repository.Services;

namespace Repository
{
    internal class RepositoryInternal
    {
        private readonly LoaderService _loaderService;
        private readonly TypeReferenceResolverService _typeReferenceResolverService;
        private readonly ClassStructResolverService _classStructResolverService;

        public RepositoryInternal(LoaderService loaderService, TypeReferenceResolverService typeReferenceResolverService, ClassStructResolverService classStructResolverService)
        {
            _loaderService = loaderService;
            _typeReferenceResolverService = typeReferenceResolverService;
            _classStructResolverService = classStructResolverService;
        }

        public IEnumerable<LoadedProject> Load(ResolveFileFunc fileFunc, IEnumerable<string> targets)
        {
            Log.Information($"Initialising repository with {targets.Count()} toplevel project(s)");

            var enumerableLoadedProjects = _loaderService.LoadOrdered(targets, fileFunc);
            var loadedProjects = enumerableLoadedProjects as List<LoadedProject> ?? enumerableLoadedProjects.ToList();
            
            _typeReferenceResolverService.Resolve(loadedProjects);
            _classStructResolverService.Resolve(loadedProjects);
            
            Log.Information($"Repository initialised with {loadedProjects.Count} top-level project(s) and {loadedProjects.Count - targets.Count()} dependencies.");
            
            return loadedProjects;
        }
    }
}
