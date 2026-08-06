using System.Collections.Generic;

namespace GirLoader;

internal static class TypeReferencesResolver
{
    public static void ResolveTypeReferences(this RepositoryTypeReferenceResolver resolver, IEnumerable<Output.AnyTypeReference> anyTypeReferences, Output.Repository repository)
    {
        foreach (var anyTypeReference in anyTypeReferences)
            resolver.ResolveTypeReference(anyTypeReference, repository);
    }
}
