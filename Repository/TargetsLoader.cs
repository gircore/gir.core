using System.Collections.Generic;
using System.Linq;
using Repository.Model;
using Repository.Services;

namespace Repository
{
    internal class TargetsLoader
    {
         private readonly LoaderService _loaderService;
        
        public TargetsLoader(LoaderService loaderService)
        {
            _loaderService = loaderService;
        }

        internal IEnumerable<Namespace> GetNamespaces(ResolveFileFunc fileFunc, IEnumerable<string> targets)
        {
            Log.Information($"Initialising repository with {targets.Count()} toplevel project(s)");

            var enumerableLoadedProjects = _loaderService.LoadOrdered(targets, fileFunc);
            var loadedProjects = enumerableLoadedProjects as List<Namespace> ?? enumerableLoadedProjects.ToList();

            TypeReferenceResolverService.Resolve(loadedProjects);
            StripProjects(loadedProjects);

            Log.Information($"Repository initialised with {loadedProjects.Count} top-level project(s) and {loadedProjects.Count - targets.Count()} dependencies.");

            return loadedProjects;
        }

        private static void StripProjects(List<Namespace> namespaces)
        {
            foreach (var ns in namespaces)
                ns.Strip();
        }
    }
}
