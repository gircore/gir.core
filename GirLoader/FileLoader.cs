using System.Collections.Generic;
using System.Linq;

namespace Gir
{
    internal class FileLoader
    {
        private readonly Output.Loader _repositoryLoader;
        private readonly Output.Resolver _repositoryResolver;

        public FileLoader(Output.Loader repositoryLoader, Output.Resolver repositoryResolver)
        {
            _repositoryLoader = repositoryLoader;
            _repositoryResolver = repositoryResolver;
        }

        internal IEnumerable<Output.Model.Repository> LoadRepositories(IEnumerable<File> files)
        {
            Log.Information($"Initialising with {files.Count()} toplevel project(s)");

            var repositories = files.Select(_repositoryLoader.LoadRepository);
            ResolveRepositories(repositories);

            return repositories;
        }

        private void ResolveRepositories(IEnumerable<Output.Model.Repository> repositories)
        {
            foreach (var repository in repositories)
                _repositoryResolver.AddRepository(repository);
            
            _repositoryResolver.Resolve();
        }
    }
}
