using System;
using System.Linq;

namespace Repository.Model
{
    internal class RepositoryFactory
    {
        private readonly NamespaceFactory _namespaceFactory;
        private readonly IncludeFactory _includeFactory;

        public RepositoryFactory(NamespaceFactory namespaceFactory, IncludeFactory includeFactory)
        {
            _namespaceFactory = namespaceFactory;
            _includeFactory = includeFactory;
        }
        
        public Repository Create(Xml.Repository repository)
        {
            if (repository.Namespace is null)
                throw new Exception($"Repository does not include any {nameof(repository.Namespace)}.");
            
            var @namespace = _namespaceFactory.CreateFromNamespace(repository.Namespace);
            var includes = repository.Includes.Select(_includeFactory.Create).ToList();
            
            return new Repository(@namespace, includes);
        }
    }
}
