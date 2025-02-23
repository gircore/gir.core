using System.Linq;
using GirLoader.Output;

namespace GirLoader;

internal static class CallbacksResolver
{
    public static void ResolveCallbacks(this RepositoryTypeReferenceResolver resolver, Repository repository)
    {
        resolver.ResolveTypeReferences(repository.Namespace.Callbacks.Select(x => x.ReturnValue.TypeReference), repository);
        resolver.ResolveParameterLists(repository.Namespace.Callbacks.Select(x => x.ParameterList), repository);
    }
}
