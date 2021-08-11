using System;
using System.Linq;

namespace GirLoader.Output
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

        public Repository Create(Input.Repository xmlRepository)
        {
            if (xmlRepository.Namespace is null)
                throw new Exception($"Repository does not include any {nameof(xmlRepository.Namespace)}.");

            var includes = xmlRepository.Includes.Select(_includeFactory.Create).ToList();
            var repository = new Repository(includes);

            //The factory sets the namespace automatically into the repository
            _ = _namespaceFactory.Create(xmlRepository.Namespace, repository);

            return repository;
        }
    }
}
