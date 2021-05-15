using System.Collections.Generic;
using System.Linq;

namespace Gir
{
    internal class FileLoader
    {
        private readonly Repository.Loader _repositoryLoader;
        private readonly Repository.Resolver _repositoryResolver;

        public FileLoader(Repository.Loader repositoryLoader, Repository.Resolver repositoryResolver)
        {
            _repositoryLoader = repositoryLoader;
            _repositoryResolver = repositoryResolver;
        }

        internal IEnumerable<Model.Repository> LoadRepositories(IEnumerable<File> files)
        {
            Log.Information($"Initialising with {files.Count()} toplevel project(s)");

            var repositories = files.Select(_repositoryLoader.Load);
            ResolveRepositories(repositories);

            return repositories;
        }

        private void ResolveRepositories(IEnumerable<Model.Repository> repositories)
        {
            foreach (var repository in repositories)
                _repositoryResolver.Add(repository);
            
            _repositoryResolver.Resolve();
        }
    }
}
