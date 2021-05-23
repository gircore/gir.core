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

        public bool TryLookup(Model.TypeReference typeReference, [MaybeNullWhen(false)] out Model.Type type)
        {
            if (_globalTypes.TryLookup(typeReference, out type))
                return true;

            if (TryResolveAlias(typeReference, out type))
                return true;

            if (typeReference.NamespaceName is null)
                return false; //If the reference has no namespace it must be a global symbol or can not be resolved

            if (!_data.TryGetValue(typeReference.NamespaceName, out var cache))
                return false;

            return cache.TryLookup(typeReference, out type);
        }

        private bool TryResolveAlias(Model.TypeReference typeReference, [MaybeNullWhen(false)] out Model.Type type)
        {
            type = null;

            if (typeReference.NamespaceName is null)
                return false;

            if (!_data.TryGetValue(typeReference.NamespaceName, out var symbolCache))
                return false;

            var repository = symbolCache.Repository;

            if (repository is null)
                throw new Exception("Namespace is missing");

            type = RecursiveResolveAlias(repository, typeReference);

            return type is { };
        }

        private Model.Type? RecursiveResolveAlias(Model.Repository repository, Model.TypeReference typeReference)
        {
            bool ResolvesReference(Model.Alias alias)
            {
                if (!string.IsNullOrEmpty(typeReference.CTypeName?.Value))
                    return typeReference.CTypeName == alias.CTypeName;//Prefer CType

                return typeReference.TypeName == alias.Name;
            }

            var directResult = repository.Namespace.Aliases.FirstOrDefault(ResolvesReference);

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
