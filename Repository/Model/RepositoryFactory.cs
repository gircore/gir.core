using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Repository.Model
{
    internal class RepositoryFactory
    {
        private readonly NamespaceFactory _namespaceFactory;
        private readonly IncludeFactory _includeFactory;
        private readonly Xml.Loader _xmlLoader;
        private readonly GetFileInfo _getFileInfo;

        public RepositoryFactory(NamespaceFactory namespaceFactory, IncludeFactory includeFactory, Xml.Loader xmlLoader, GetFileInfo getFileInfo)
        {
            _namespaceFactory = namespaceFactory;
            _includeFactory = includeFactory;
            _xmlLoader = xmlLoader;
            _getFileInfo = getFileInfo;
        }
        
        public Repository Create(FileInfo girFile)
        {
            Xml.Repository repository = _xmlLoader.LoadRepository(girFile);
            return Create(repository);
        }
        
        private Repository Create(Xml.Repository repository)
        {
            if (repository.Namespace is null)
                throw new Exception($"Repository does not include any {nameof(repository.Namespace)}.");
            
            var @namespace = _namespaceFactory.CreateFromNamespace(repository.Namespace);
            var includes = repository.Includes.Select(_includeFactory.Create).ToList();
            ResolveIncludes(includes);

            return new Repository(@namespace, includes);
        }

        private void ResolveIncludes(IEnumerable<Include> includes)
        {
            foreach (var include in includes)
                include.Resolve(Create(include));
        }

        private Repository Create(Include include)
        {
            var includeFileInfo = _getFileInfo(include);
            return Create(includeFileInfo);
        }
    }
}
