using System.Collections.Generic;

namespace Repository
{
    internal class RepositoryLoader
    {
        private readonly Xml.Loader _xmlLoader;
        private readonly GetGirFile _getGirFile;
        private readonly Model.RepositoryFactory _repositoryFactory;
        private readonly Dictionary<GirFile, Model.Repository> _knownRepositories = new ();

        public RepositoryLoader(Xml.Loader xmlLoader, GetGirFile getGirFile, Model.RepositoryFactory repositoryFactory)
        {
            _xmlLoader = xmlLoader;
            _getGirFile = getGirFile;
            _repositoryFactory = repositoryFactory;
        }

        public Model.Repository Load(GirFile girFile)
        {
            if (_knownRepositories.TryGetValue(girFile, out Model.Repository repository))
                return repository;

            repository = Create(girFile);
            _knownRepositories[girFile] = repository;

            return repository;
        }

        private Model.Repository Create(GirFile girFile)
        {
            Xml.Repository xmlRepository = _xmlLoader.LoadRepository(girFile);
            var repository = _repositoryFactory.Create(xmlRepository);
            ResolveIncludes(repository.Includes);
            return repository;
        }

        private void ResolveIncludes(IEnumerable<Model.Include> includes)
        {
            foreach (var include in includes)
                include.Resolve(Load(include));
        }

        private Model.Repository Load(Model.Include include)
        {
            var includeFileInfo = _getGirFile(include);
            return Load(includeFileInfo);
        }
    }
}
