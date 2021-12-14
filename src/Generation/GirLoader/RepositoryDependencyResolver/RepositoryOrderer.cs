using System.Collections.Generic;

namespace GirLoader.Helper
{
    internal static class RepositoryOrderer
    {
        public static List<Output.Repository> OrderByDependencies(this IEnumerable<Output.Repository> unorderedRepositories)
        {
            var dependencyResolver = new RepositoryDependencyResolver(unorderedRepositories);
            return dependencyResolver.ResolveOrdered();
        }
    }
}
