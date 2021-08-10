using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace GirLoader
{
    internal partial class TypeReferenceResolver
    {
        private readonly Dictionary<Output.Repository, TypeCache> _data = new();
        private readonly TypeCache _globalTypes = new GlobalTypeCache();

        public bool Resolve(Output.TypeReference typeReference, Output.Repository repository, [MaybeNullWhen(false)] out Output.Type type)
        {
            if (_globalTypes.TryResolve(typeReference, out type))
                return true;

            if (TryResolveAlias(typeReference, repository, out type))
                return true;

            //Prefer current namespace to resolve types before trying other ones
            if (_data.TryGetValue(repository, out var typeCache))
                if (typeCache.TryResolve(typeReference, out type))
                    return true;

            foreach (var cache in _data.Values)
            {
                if (cache.TryResolve(typeReference, out type))
                    return true;
            }

            return false;
        }

        private bool TryResolveAlias(Output.TypeReference typeReference, Output.Repository repository, [MaybeNullWhen(false)] out Output.Type type)
        {
            type = RecursiveResolveAlias(repository, typeReference);

            return type is { };
        }

        private static Output.Type? RecursiveResolveAlias(Output.Repository repository, Output.TypeReference typeReference)
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

        public void AddRepository(Output.Repository repository)
        {
            if (_data.ContainsKey(repository))
                throw new Exception($"The repository contains an already known repository {repository}.");

            _data.Add(repository, new RepositoryTypeCache(repository));
        }
    }
}
