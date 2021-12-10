using GirLoader.Output;

namespace GirLoader
{
    internal static class RepositoryAliasesResolver
    {
        public static void ResolveAliases(this RepositoryResolver resolver, Repository repository)
        {
            foreach (var alias in repository.Namespace.Aliases)
                resolver.ResolveTypeReference(alias.TypeReference, repository);
        }
    }
}
