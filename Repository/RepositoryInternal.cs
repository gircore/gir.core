using System.Collections.Generic;
using System.Linq;
using Repository.Services;

namespace Repository
{
    public class RepositoryInternal
    {
        private readonly ILoaderService _loaderService;
        private readonly ITypeReferenceResolverService _typeReferenceResolverService;

        public RepositoryInternal(ILoaderService loaderService, ITypeReferenceResolverService typeReferenceResolverService)
        {
            _loaderService = loaderService;
            _typeReferenceResolverService = typeReferenceResolverService;
        }

        public IEnumerable<ILoadedProject> Load(ResolveFileFunc fileFunc, string[] targets)
        {
            Log.Information($"Initialising generator with {targets.Length} toplevel project(s)");

            var enumerableLoadedProjects = _loaderService.LoadOrdered(targets, fileFunc);
            var loadedProjects = enumerableLoadedProjects as List<ILoadedProject> ?? enumerableLoadedProjects.ToList();
            
            _typeReferenceResolverService.Resolve(loadedProjects);
            
            Log.Information($"Repository initialised with {loadedProjects.Count} top-level project(s) and {loadedProjects.Count - targets.Length} dependencies.");
            
            return loadedProjects;
        }
    }
}
