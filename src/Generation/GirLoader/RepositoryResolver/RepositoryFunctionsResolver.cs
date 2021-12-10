using System.Linq;
using GirLoader.Output;

namespace GirLoader
{
    internal static class RepositoryFunctionsResolver
    {
        public static void ResolveFunctions(this RepositoryResolver resolver, Repository repository)
        {
            resolver.ResolveTypeReferences(repository.Namespace.Functions.Select(x => x.ReturnValue.TypeReference), repository);
            resolver.ResolveParameterLists(repository.Namespace.Functions.Select(x => x.ParameterList), repository);
        }
    }
}
