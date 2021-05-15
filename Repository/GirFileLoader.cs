using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    internal class GirFileLoader
    {
        private readonly RepositoryLoader _repositoryLoader;
        private readonly RepositoryResolver _repositoryResolver;

        public GirFileLoader(RepositoryLoader repositoryLoader, RepositoryResolver repositoryResolver)
        {
            _repositoryLoader = repositoryLoader;
            _repositoryResolver = repositoryResolver;
        }

        internal IEnumerable<Model.Repository> LoadRepositories(IEnumerable<GirFile> girFiles)
        {
            Log.Information($"Initialising with {girFiles.Count()} toplevel project(s)");

            var repositories = girFiles.Select(_repositoryLoader.Load);
            ResolveRepositories(repositories);

            return repositories;
        }

        private void ResolveRepositories(IEnumerable<Model.Repository> repositories)
        {
            foreach (var repository in repositories)
                _repositoryResolver.Load(repository);
            
            _repositoryResolver.Resolve();
        }
    }
}
