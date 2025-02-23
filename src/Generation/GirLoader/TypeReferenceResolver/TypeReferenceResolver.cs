using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GirLoader.Helper;

namespace GirLoader;

internal partial class TypeReferenceResolver
{
    private readonly Dictionary<Output.Repository, TypeCache> _typeCaches = new();
    private readonly TypeCache _globalTypes = new GlobalTypeCache();

    public bool Resolve(Output.TypeReference typeReference, Output.Repository repository, [MaybeNullWhen(false)] out Output.Type type)
    {
        if (TryResolveFromType(typeReference, repository, out type))
            return true;

        if (TryResolveFromAlias(typeReference, repository, out type))
            return true;

        if (TryResolveFromForeignType(typeReference, repository, out type))
            return true;

        if (TryResolveFromForeignAlias(typeReference, repository, out type))
            return true;

        if (_globalTypes.TryResolve(typeReference, out type))
            return true;

        return false;
    }

    private bool TryResolveFromType(Output.TypeReference typeReference, Output.Repository repository, [MaybeNullWhen(false)] out Output.Type type)
    {
        if (_typeCaches.TryGetValue(repository, out var typeCache))
            if (typeCache.TryResolve(typeReference, out type))
                return true;

        type = null;
        return false;
    }

    private bool TryResolveFromAlias(Output.TypeReference typeReference, Output.Repository repository, [MaybeNullWhen(false)] out Output.Type type)
    {
        type = repository.Namespace.Aliases.FirstOrDefault(x => x.Matches(typeReference));

        return type is not null;
    }

    private bool TryResolveFromForeignType(Output.TypeReference typeReference, Output.Repository repository, [MaybeNullWhen(false)] out Output.Type type)
    {
        var orderedDependentRepositories = repository.Dependencies.OrderByDependencies();
        orderedDependentRepositories.Reverse(); //Reverse order to iterate from most specific to most generic dependency

        foreach (var dependentRepository in orderedDependentRepositories)
            if (_typeCaches.TryGetValue(dependentRepository, out var typeCache))
                if (typeCache.TryResolve(typeReference, out type))
                    return true;

        type = null;
        return false;
    }

    private bool TryResolveFromForeignAlias(Output.TypeReference typeReference, Output.Repository repository, [MaybeNullWhen(false)] out Output.Type type)
    {
        var orderedDependentRepositories = repository.Dependencies.OrderByDependencies();
        orderedDependentRepositories.Reverse(); //Reverse order to iterate from most specific to most generic dependency

        foreach (var r in orderedDependentRepositories)
        {
            if (TryResolveFromAlias(typeReference, r, out type))
                return true;
        }

        type = null;
        return false;
    }

    public void AddRepository(Output.Repository repository)
    {
        if (_typeCaches.ContainsKey(repository))
            throw new Exception($"The repository contains an already known repository {repository}.");

        _typeCaches.Add(repository, new RepositoryTypeCache(repository));
    }
}
