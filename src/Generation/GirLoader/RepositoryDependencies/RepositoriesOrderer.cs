using System.Collections.Generic;

namespace GirLoader.Helper
{
    internal static class RepositoriesOrderer
    {
        public static List<Output.Repository> OrderByDependencies(this IEnumerable<Output.Repository> unorderedRepositories)
        {
            var dependencyResolver = new RepositoriesDependencyResolver(unorderedRepositories);
            return dependencyResolver.ResolveOrdered();
        }
    }
}
