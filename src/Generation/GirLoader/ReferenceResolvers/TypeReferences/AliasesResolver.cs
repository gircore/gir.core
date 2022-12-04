using GirLoader.Output;

namespace GirLoader;

internal static class AliasesResolver
{
    public static void ResolveAliases(this RepositoryTypeReferenceResolver resolver, Repository repository)
    {
        foreach (var alias in repository.Namespace.Aliases)
            resolver.ResolveTypeReference(alias.TypeReference, repository);
    }
}
