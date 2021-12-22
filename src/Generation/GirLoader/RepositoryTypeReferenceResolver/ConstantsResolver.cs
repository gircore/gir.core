using System.Linq;
using GirLoader.Output;

namespace GirLoader
{
    internal static class ConstantsResolver
    {
        public static void ResolveConstants(this RepositoryTypeReferenceResolver resolver, Repository repository)
        {
            resolver.ResolveTypeReferences(repository.Namespace.Constants.Select(x => x.TypeReference), repository);
        }
    }
}
