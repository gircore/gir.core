using System.Collections.Generic;

namespace Gir.Repository
{
    internal class Loader
    {
        private readonly Xml.Loader _xmlLoader;
        private readonly GetGirFile _getGirFile;
        private readonly Model.RepositoryFactory _repositoryFactory;
        private readonly Dictionary<File, Model.Repository> _knownRepositories = new ();

        public Loader(Xml.Loader xmlLoader, GetGirFile getGirFile, Model.RepositoryFactory repositoryFactory)
        {
            _xmlLoader = xmlLoader;
            _getGirFile = getGirFile;
            _repositoryFactory = repositoryFactory;
        }

        public Model.Repository Load(File file)
        {
            if (_knownRepositories.TryGetValue(file, out Model.Repository repository))
                return repository;

            repository = Create(file);
            _knownRepositories[file] = repository;

            return repository;
        }

        private Model.Repository Create(File file)
        {
            Xml.Repository xmlRepository = _xmlLoader.LoadRepository(file);
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
