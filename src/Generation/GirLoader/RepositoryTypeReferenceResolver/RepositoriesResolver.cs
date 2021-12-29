using System;
using System.Collections.Generic;

namespace GirLoader
{
    internal static class RepositoriesResolver
    {
        public static void ResolveTypeReferences(this List<Output.Repository> repositories)
        {
            var repositoryResolver = new RepositoryTypeReferenceResolver(repositories);
            foreach (var repository in repositories)
            {
                repositoryResolver.ResolveAliases(repository);
                repositoryResolver.ResolveCallbacks(repository);
                repositoryResolver.ResolveClasses(repository);
                repositoryResolver.ResolveInterfaces(repository);
                repositoryResolver.ResolveRecords(repository);
                repositoryResolver.ResolveFunctions(repository);
                repositoryResolver.ResolveConstants(repository);
                repositoryResolver.ResolveUnions(repository);
            }
        }
    }
}
