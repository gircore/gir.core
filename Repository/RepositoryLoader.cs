using System.Collections.Generic;
using System.IO;
using System.Linq;
using Repository.Graph;
using Repository.Services;

namespace Repository
{
    internal class RepositoryLoader
    {
        private readonly Model.RepositoryFactory _repositoryFactory;

        public RepositoryLoader(Model.RepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
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
            return girFiles.Select(_repositoryFactory.Create);
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
