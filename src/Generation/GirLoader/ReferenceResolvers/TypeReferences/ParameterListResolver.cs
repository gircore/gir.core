using System.Collections.Generic;
using System.Linq;
using GirLoader.Output;

namespace GirLoader;

internal static class ParameterListResolver
{
    public static void ResolveParameterList(this RepositoryTypeReferenceResolver resolver, ParameterList parameterList, Repository repository)
    {
        if (parameterList.InstanceParameter is not null)
            resolver.ResolveTypeReference(parameterList.InstanceParameter.TypeReference, repository);

        resolver.ResolveTypeReferences(parameterList.SingleParameters.Select(x => x.TypeReference), repository);
    }

    public static void ResolveParameterLists(this RepositoryTypeReferenceResolver resolver, IEnumerable<ParameterList> parameterLists, Repository repository)
    {
        foreach (var parameterList in parameterLists)
            ResolveParameterList(resolver, parameterList, repository);
    }
}
