using System;
using System.Collections.Generic;
using System.Linq;

namespace GirLoader;

internal class RepositoryTypeReferenceResolver
{
    private readonly TypeReferenceResolver _typeReferenceResolver = new();
    private readonly HashSet<Output.Repository> _knownRepositories = new();

    public RepositoryTypeReferenceResolver(IEnumerable<Output.Repository> repositories)
    {
        foreach (var repository in repositories)
        {
            Add(repository);
        }
    }

    /// <summary>
    /// Loads the given repository and all its dependencies
    /// </summary>
    private void Add(Output.Repository repository)
    {
        if (!_knownRepositories.Add(repository))
            return; //Ignore known repositories

        _typeReferenceResolver.AddRepository(repository);

        foreach (var dependentRepository in repository.Includes.Select(x => x.GetResolvedRepository()))
            Add(dependentRepository);
    }

    public void ResolveTypeReference(Output.TypeReference reference, Output.Repository repository)
    {
        foreach (var elementTypeReference in reference.ElementTypeReferences)
            ResolveTypeReference(elementTypeReference, repository);

        if (reference is Output.ResolveableTypeReference resolveableTypeReference)
        {
            if (_typeReferenceResolver.Resolve(resolveableTypeReference, repository, out var type))
                resolveableTypeReference.ResolveAs(type);
            else
                Log.Verbose($"Could not resolve type reference {reference}");
        }
        else if (reference is not Output.ArrayTypeReference)
        {
            // Array type references are not resolved directly. Only their element type
            // gets resolved above because arrays are no type themself. They only provide
            // structure.
            throw new Exception($"Unknown {nameof(Output.TypeReference)} {reference.GetType().Name}");
        }
    }
}
