using System.Collections.Generic;
using System.IO;
using System.Linq;
using Repository.Graph;

namespace Repository
{
    //TODO Delete?
    internal class RepositoriesLoader
    {
        private readonly RepositoryLoader _repositoryLoader;

        public RepositoriesLoader(RepositoryLoader repositoryLoader)
        {
            _repositoryLoader = repositoryLoader;
        }

        internal IEnumerable<Model.Repository> GetRepositories(IEnumerable<FileInfo> girFiles)
        {
            Log.Information($"Initialising with {girFiles.Count()} toplevel project(s)");

            var repositories = CreateRepositories(girFiles);
            repositories = OrderRepositories(repositories);
            ResolveRepositories(repositories);

            return repositories;
        }

        private IEnumerable<Model.Repository> CreateRepositories(IEnumerable<FileInfo> girFiles)
        {
            return girFiles.Select(_repositoryLoader.Load);
        }

        private IEnumerable<Model.Repository> OrderRepositories(IEnumerable<Model.Repository> repositories)
        {
            var resolverService = new DependencyResolverService<Model.Repository>();
            return resolverService.ResolveOrdered(repositories).Cast<Model.Repository>();
        }

        private void ResolveRepositories(IEnumerable<Model.Repository> repositories)
        {
            var repositoryResolver = new RepositoryResolver();
            foreach (var repository in repositories)
                repositoryResolver.Resolve(repository);
        }
    }
}
