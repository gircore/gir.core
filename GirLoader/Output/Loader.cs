using System.Collections.Generic;
using System.Linq;

namespace GirLoader.Output
{
    internal class Loader
    {
        private readonly RepositoryLoader _repositoryLoader;
        private readonly RepositoryResolver _repositoryResolver;

        public Loader(RepositoryLoader repositoryLoader, RepositoryResolver repositoryResolver)
        {
            _repositoryLoader = repositoryLoader;
            _repositoryResolver = repositoryResolver;
        }

        internal IEnumerable<Model.Repository> LoadRepositories(IEnumerable<Input.Model.Repository> inputRepositories)
        {
            Log.Information($"Initialising with {inputRepositories.Count()} toplevel repositories");

            var outputRepositories = inputRepositories.Select(_repositoryLoader.LoadRepository);
            ResolveRepositories(outputRepositories);

            return outputRepositories;
        }

        private void ResolveRepositories(IEnumerable<Model.Repository> repositories)
        {
            foreach (var repository in repositories)
                _repositoryResolver.Add(repository);

            _repositoryResolver.Resolve();
        }
    }
}
