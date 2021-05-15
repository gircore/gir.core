using System.Collections.Generic;
using System.IO;

namespace Repository
{
    internal class RepositoryLoader
    {
        private readonly Xml.Loader _xmlLoader;
        private readonly GetFileInfo _getFileInfo;
        private readonly Model.RepositoryFactory _repositoryFactory;
        private readonly Dictionary<FileInfo, Model.Repository> _knownRepositories = new ();

        public RepositoryLoader(Xml.Loader xmlLoader, GetFileInfo getFileInfo, Model.RepositoryFactory repositoryFactory)
        {
            _xmlLoader = xmlLoader;
            _getFileInfo = getFileInfo;
            _repositoryFactory = repositoryFactory;
        }
        
        public Model.Repository Load(FileInfo girFile)
        {
            if (_knownRepositories.TryGetValue(girFile, out Model.Repository repository))
                return repository;

            repository = Create(girFile);
            _knownRepositories[girFile] = repository;
            
            return repository;
        }

        private Model.Repository Create(FileInfo girFile)
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
            var includeFileInfo = _getFileInfo(include);
            return Load(includeFileInfo);
        }
    }
}
