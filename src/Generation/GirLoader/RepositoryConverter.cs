using System.Collections.Generic;

namespace GirLoader
{
    internal class RepositoryConverter
    {
        private readonly ResolveInclude _resolveInclude;
        private readonly Output.RepositoryFactory _repositoryFactory;
        private readonly Dictionary<string, Output.Repository> _knownRepositories = new();

        public RepositoryConverter(ResolveInclude resolveInclude, Output.RepositoryFactory repositoryFactory)
        {
            _resolveInclude = resolveInclude;
            _repositoryFactory = repositoryFactory;
        }

        public Output.Repository LoadRepository(Input.Repository inputRepository)
        {
            if (_knownRepositories.TryGetValue(inputRepository.ToString(), out Output.Repository? repository))
                return repository;

            repository = Create(inputRepository);
            _knownRepositories[inputRepository.ToString()] = repository;

            return repository;
        }

        private Output.Repository Create(Input.Repository inputRepository)
        {
            var repository = _repositoryFactory.Create(inputRepository);
            ResolveIncludes(repository.Includes);
            Log.Debug($"Created repository {repository.Namespace.Name}.");
            return repository;
        }

        private void ResolveIncludes(IEnumerable<Output.Include> includes)
        {
            foreach (var include in includes)
                include.Resolve(LoadRepository(include));
        }

        private Output.Repository LoadRepository(Output.Include include)
        {
            var includeFileInfo = _resolveInclude(include);
            return LoadRepository(includeFileInfo);
        }
    }
}
