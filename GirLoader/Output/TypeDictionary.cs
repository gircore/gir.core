using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace GirLoader.Output
{
    internal partial class TypeDictionary
    {
        private readonly Dictionary<Model.NamespaceName, TypeCache> _data = new();
        private readonly TypeCache _globalTypes = new(null);

        public bool TryLookup(Model.TypeReference typeReference, Model.NamespaceName currentNamespace, [MaybeNullWhen(false)] out Model.Type type)
        {
            if (_globalTypes.TryLookup(typeReference, out type))
                return true;

            if (TryResolveAlias(typeReference, currentNamespace, out type))
                return true;

            foreach (var cache in _data.Values)
            {
                if (cache.TryLookup(typeReference, out type))
                    return true;
            }

            return false;
        }

        private bool TryResolveAlias(Model.TypeReference typeReference, Model.NamespaceName currentNamespace, [MaybeNullWhen(false)] out Model.Type type)
        {
            type = null;

            if (!_data.TryGetValue(currentNamespace, out var symbolCache))
                return false;

            var repository = symbolCache.Repository;

            if (repository is null)
                throw new Exception("Namespace is missing");

            type = RecursiveResolveAlias(repository, typeReference);

            return type is { };
        }

        private Model.Type? RecursiveResolveAlias(Model.Repository repository, Model.TypeReference typeReference)
        {
            var directResult = repository.Namespace.Aliases.FirstOrDefault(x => x.Matches(typeReference));

            if (directResult is { })
                return directResult.TypeReference.GetResolvedType();

            foreach (var parent in repository.Dependencies)
            {
                var parentResult = RecursiveResolveAlias(parent, typeReference);
                if (parentResult is { })
                    return parentResult;
            }

            return null;
        }

        public void AddTypes(IEnumerable<Model.Type> types)
        {
            foreach (var type in types)
                AddType(type);
        }

        public void AddType(Model.Type type)
        {
            if (type.Repository is null)
                AddGlobalType(type);
            else
                AddConcreteType(type);
        }

        private void AddGlobalType(Model.Type type)
        {
            Debug.Assert(
                condition: type.Repository is null,
                message: "A default symbol is not allowed to have a namespace"
            );

            _globalTypes.Add(type);
        }

        private void AddConcreteType(Model.Type type)
        {
            Debug.Assert(
                condition: type.Repository is not null,
                message: "A concrete symbol is must have a namespace"
            );

            if (!_data.TryGetValue(type.Repository.Namespace.Name, out var cache))
            {
                cache = new TypeCache(type.Repository);
                _data[type.Repository.Namespace.Name] = cache;
            }

            cache.Add(type);
        }
    }
}
