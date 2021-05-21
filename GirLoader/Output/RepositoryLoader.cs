using System.Collections.Generic;

namespace GirLoader.Output
{
    internal class RepositoryLoader
    {
        private readonly Input.Loader _inputLoader;
        private readonly GetGirFile _getGirFile;
        private readonly Model.RepositoryFactory _repositoryFactory;
        private readonly Dictionary<GirFile, Model.Repository> _knownRepositories = new ();

        public RepositoryLoader(Input.Loader inputLoader, GetGirFile getGirFile, Model.RepositoryFactory repositoryFactory)
        {
            _inputLoader = inputLoader;
            _getGirFile = getGirFile;
            _repositoryFactory = repositoryFactory;
        }

        public Model.Repository LoadRepository(GirFile girFile)
        {
            if (_knownRepositories.TryGetValue(girFile, out Model.Repository repository))
                return repository;

            repository = Create(girFile);
            _knownRepositories[girFile] = repository;

            return repository;
        }

        private Model.Repository Create(GirFile girFile)
        {
            Input.Model.Repository xmlRepository = _inputLoader.LoadRepository(girFile);
            var repository = _repositoryFactory.Create(xmlRepository);
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
            var includeFileInfo = _getGirFile(include);
            return LoadRepository(includeFileInfo);
        }
    }
}
