using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Repository.Analysis
{
    public partial class TypeDictionary
    {
        private readonly Dictionary<NamespaceName, TypeCache> _data = new();
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

            var ns = symbolCache.Namespace;

            if (ns is null)
                throw new Exception("Namespace is missing");

            type = RecursiveResolveAlias(ns, typeReference);

            return type is { };
        }

        private Model.Type? RecursiveResolveAlias(Model.Namespace ns, Model.TypeReference typeReference)
        {
            var directResult = ns.Aliases.FirstOrDefault(x => Resolves(x, typeReference));

            if (directResult is { })
                return directResult.TypeReference.GetResolvedType();

            foreach (var parent in ns.Dependencies)
            {
                var parentResult = RecursiveResolveAlias(parent, typeReference);
                if (parentResult is { })
                    return parentResult;
            }

            return null;
        }

        private static bool Resolves(Model.Alias alias, Model.TypeReference typeReference)
        {
            return (typeReference.TypeName == alias.SymbolName) || (typeReference.CTypeName == alias.Name);
        }

        public void AddTypes(IEnumerable<Model.Type> types)
        {
            foreach (var type in types)
                AddType(type);
        }

        public void AddType(Model.Type type)
        {
            if (type.Namespace is null)
                AddGlobalType(type);
            else
                AddConcreteType(type);
        }

        private void AddGlobalType(Model.Type type)
        {
            Debug.Assert(
                condition: type.Namespace is null,
                message: "A default symbol is not allowed to have a namespace"
            );

            _globalTypes.Add(type);
        }

        private void AddConcreteType(Model.Type type)
        {
            Debug.Assert(
                condition: type.Namespace is not null,
                message: "A concrete symbol is must have a namespace"
            );

            if (!_data.TryGetValue(type.Namespace.Name, out var cache))
            {
                cache = new TypeCache(type.Namespace);
                _data[type.Namespace.Name] = cache;
            }

            cache.Add(type);
        }

        public void ResolveAliases(IEnumerable<Model.Alias> aliases)
        {
            foreach (var alias in aliases)
            {
                if (TryLookup(alias.TypeReference, out var symbol))
                    alias.TypeReference.ResolveAs(symbol);
            }
        }
    }
}
