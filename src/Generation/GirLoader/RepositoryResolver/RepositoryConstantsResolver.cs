using System.Linq;
using GirLoader.Output;

namespace GirLoader
{
    internal static class RepositoryConstantsResolver
    {
        public static void ResolveConstants(this RepositoryResolver resolver, Repository repository)
        {
            resolver.ResolveTypeReferences(repository.Namespace.Constants.Select(x => x.TypeReference), repository);
        }
    }
}
