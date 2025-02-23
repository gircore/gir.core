using System.Collections.Generic;

namespace GirLoader;

internal partial class TypeReferenceResolver
{
    private class RepositoryTypeCache : TypeCache
    {
        private readonly Output.Repository _repository;

        public RepositoryTypeCache(Output.Repository repository)
        {
            _repository = repository;
            Add(repository.Namespace.Classes);
            Add(repository.Namespace.Interfaces);
            Add(repository.Namespace.Callbacks);
            Add(repository.Namespace.Enumerations);
            Add(repository.Namespace.Bitfields);
            Add(repository.Namespace.Records);
            Add(repository.Namespace.Unions);
        }

        private void Add(IEnumerable<Output.Type> types)
        {
            foreach (var type in types)
                Add(type);
        }

        public override string ToString()
        {
            return $"{nameof(TypeCache)} for: {_repository.Namespace.Name}";
        }
    }
}
