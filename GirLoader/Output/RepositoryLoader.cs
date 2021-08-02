using System.Collections.Generic;

namespace GirLoader.Output
{
    internal class RepositoryLoader
    {
        private readonly ResolveInclude _resolveInclude;
        private readonly Model.RepositoryFactory _repositoryFactory;
        private readonly Dictionary<string, Model.Repository> _knownRepositories = new();

        public RepositoryLoader(ResolveInclude resolveInclude, Model.RepositoryFactory repositoryFactory)
        {
            _resolveInclude = resolveInclude;
            _repositoryFactory = repositoryFactory;
        }

        public Model.Repository LoadRepository(Input.Model.Repository inputRepository)
        {
            if (_knownRepositories.TryGetValue(inputRepository.ToString(), out Model.Repository? repository))
                return repository;

            repository = Create(inputRepository);
            _knownRepositories[inputRepository.ToString()] = repository;

            return repository;
        }

        private Model.Repository Create(Input.Model.Repository inputRepository)
        {
            var repository = _repositoryFactory.Create(inputRepository);
            ResolveIncludes(repository.Includes);
            Log.Debug($"Created repository {repository.Namespace.Name}.");
            return repository;
        }

        private void ResolveIncludes(IEnumerable<Model.Include> includes)
        {
            foreach (var include in includes)
                include.Resolve(LoadRepository(include));
        }

        private Model.Repository LoadRepository(Model.Include include)
        {
            var includeFileInfo = _resolveInclude(include);
            return LoadRepository(includeFileInfo);
        }
    }
}
