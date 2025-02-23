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

        //Ignore variadic arguments as those don't need to be resolved
        var typeReferences = parameterList.SingleParameters
            .Select(x => x.TypeReferenceOrVarArgs)
            .Where(x => x.IsT0)
            .Select(x => x.AsT0);

        resolver.ResolveTypeReferences(typeReferences, repository);
    }

    public static void ResolveParameterLists(this RepositoryTypeReferenceResolver resolver, IEnumerable<ParameterList> parameterLists, Repository repository)
    {
        foreach (var parameterList in parameterLists)
            ResolveParameterList(resolver, parameterList, repository);
    }
}
