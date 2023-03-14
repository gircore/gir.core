using System.Collections.Generic;

namespace GirLoader;

internal static class RepositoriesResolver
{
    public static void ResolveReferences(this List<Output.Repository> repositories)
    {
        var repositoryTypeReferenceResolver = new RepositoryTypeReferenceResolver(repositories);
        foreach (var repository in repositories)
        {
            repositoryTypeReferenceResolver.ResolveAliases(repository);
            repositoryTypeReferenceResolver.ResolveCallbacks(repository);
            repositoryTypeReferenceResolver.ResolveClasses(repository);
            repositoryTypeReferenceResolver.ResolveInterfaces(repository);
            repositoryTypeReferenceResolver.ResolveRecords(repository);
            repositoryTypeReferenceResolver.ResolveFunctions(repository);
            repositoryTypeReferenceResolver.ResolveConstants(repository);
            repositoryTypeReferenceResolver.ResolveUnions(repository);

            repository.Namespace.Classes.ResolveAccessors();
            repository.Namespace.Interfaces.ResolveAccessors();

            ShadowableResolver.Resolve(repository.Namespace.Classes);
            ShadowableResolver.Resolve(repository.Namespace.Interfaces);
            ShadowableResolver.Resolve(repository.Namespace.Records);
            ShadowableResolver.Resolve(new[] { new ShadowableAdapter(repository.Namespace.Functions) });

        }
    }
}
