using System.Collections.Generic;
using System.IO;
using System.Linq;
using Repository.Graph;
using Repository.Services;

namespace Repository
{
    internal class TargetsLoader2
    {
        private readonly Model.RepositoryFactory _repositoryFactory;

        public TargetsLoader2(Model.RepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        internal IEnumerable<Model.Repository> GetRepositories(IEnumerable<FileInfo> girFiles)
        {
            Log.Information($"Initialising with {girFiles.Count()} toplevel project(s)");

            var repositories = CreateRepositories(girFiles);
            repositories = OrderRepositories(repositories);
            ResolveTypeReferences(repositories);
            
            return repositories;
        }

        private IEnumerable<Model.Repository> CreateRepositories(IEnumerable<FileInfo> girFiles)
        {
            return girFiles.Select(_repositoryFactory.Create);
        }

        private IEnumerable<Model.Repository> OrderRepositories(IEnumerable<Model.Repository> repositories)
        {
            var resolverService = new DependencyResolverService<Model.Repository>();
            return resolverService.ResolveOrdered(repositories).Cast<Model.Repository>();
        }

        private void ResolveTypeReferences(IEnumerable<Model.Repository> repositories)
        {
            
        }
    }
    internal class TargetsLoader
    {
         private readonly LoaderService _loaderService;
        
        public TargetsLoader(LoaderService loaderService)
        {
            _loaderService = loaderService;
        }

        internal IEnumerable<Model.Namespace> GetNamespaces(ResolveFileFunc fileFunc, IEnumerable<string> targets)
        {
            Log.Information($"Initialising repository with {targets.Count()} toplevel project(s)");

            var enumerableLoadedProjects = _loaderService.LoadOrdered(targets, fileFunc);
            var loadedProjects = enumerableLoadedProjects as List<Model.Namespace> ?? enumerableLoadedProjects.ToList();

            TypeReferenceResolverService.Resolve(loadedProjects);
            StripProjects(loadedProjects);

            Log.Information($"Repository initialised with {loadedProjects.Count} top-level project(s) and {loadedProjects.Count - targets.Count()} dependencies.");

            return loadedProjects;
        }

        private static void StripProjects(List<Model.Namespace> namespaces)
        {
            foreach (var ns in namespaces)
                ns.Strip();
        }
    }
}
