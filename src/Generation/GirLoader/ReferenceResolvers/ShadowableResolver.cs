using System;
using System.Collections.Generic;
using System.Linq;
using GirLoader.Output;

namespace GirLoader;

internal static class ShadowableResolver
{
    public static void Resolve(IEnumerable<ShadowableProvider> shadowableProviders)
    {
        foreach (var shadowableProvider in shadowableProviders)
        {
            ResolveShadowsReferences(shadowableProvider);
            ResolveShadowedByReferences(shadowableProvider);
        }
    }

    private static void ResolveShadowsReferences(ShadowableProvider shadowableProvider)
    {
        var shadowsReferences = shadowableProvider.Functions.Select(x => x.ShadowsReference)
            .Union(shadowableProvider.Methods.Select(x => x.ShadowsReference))
            .Union((shadowableProvider.Constructors ?? Enumerable.Empty<Constructor>()).Select(x => x.ShadowsReference))
            .OfType<ShadowsReference>()
            .ToList();

        foreach (var shadowsReference in shadowsReferences)
            ResolveShadowsReference(shadowsReference, shadowableProvider);
    }

    private static void ResolveShadowsReference(ShadowsReference reference, ShadowableProvider shadowableProvider)
    {
        var matchingFunctions = ((IEnumerable<Callable>) shadowableProvider.Functions)
            .Union(shadowableProvider.Methods)
            .Union(shadowableProvider.Constructors ?? Enumerable.Empty<Callable>())
            .Where(callable => callable.Name == reference.Name)
            .ToList();

        if (matchingFunctions.Count > 1)
            throw new Exception($"Found multiple functions to resolve \"shadows\" reference \"{reference.Name}\". This probably means there is an error in the gir file.");

        if (matchingFunctions.Count == 0)
            throw new Exception($"Found no functions to resolve \"shadows\" reference \"{reference.Name}\". This probably means there is an error in the gir file.");

        var matchingFunction = matchingFunctions.First();

        if (matchingFunction.ShadowedByReference is null)
            throw new Exception($"Found a functions to resolve \"shadows\" reference \"{reference.Name}\" but it is missing the \"shadowed-by\" attribute. This probably means there is an error in the gir file.");

        if (matchingFunction.ShadowedByReference.Name != reference.GetParentCallable().Name)
            throw new Exception($"Found a functions to resolve \"shadows\" reference \"{reference.Name}\" but it has the wrong content in the \"shadowed-by\" attribute. Expected: {reference.GetParentCallable().Name}, Found: {matchingFunction.ShadowedByReference.Name}. This probably means there is an error in the gir file.");

        reference.Resolve(matchingFunctions.First());
    }

    private static void ResolveShadowedByReferences(ShadowableProvider shadowableProvider)
    {
        var shadowedByReferences = shadowableProvider.Functions.Select(x => x.ShadowedByReference)
            .Union(shadowableProvider.Methods.Select(x => x.ShadowedByReference))
            .Union((shadowableProvider.Constructors ?? Enumerable.Empty<Constructor>()).Select(x => x.ShadowedByReference))
            .OfType<ShadowedByReference>()
            .ToList();

        foreach (var shadowedByReference in shadowedByReferences)
            ResolveShadowedByReference(shadowedByReference, shadowableProvider);
    }

    private static void ResolveShadowedByReference(ShadowedByReference reference, ShadowableProvider shadowableProvider)
    {
        var matchingFunctions = ((IEnumerable<Callable>) shadowableProvider.Functions)
            .Union(shadowableProvider.Methods)
            .Union(shadowableProvider.Constructors ?? Enumerable.Empty<Callable>())
            .Where(callable => callable.Name == reference.Name)
            .ToList();

        if (matchingFunctions.Count > 1)
            throw new Exception($"Found multiple functions to resolve \"shadowed-by\" reference \"{reference.Name}\". This probably means there is an error in the gir file.");

        if (matchingFunctions.Count == 0)
            throw new Exception($"Found no functions to resolve \"shadowed-by\" reference \"{reference.Name}\". This probably means there is an error in the gir file.");

        var matchingFunction = matchingFunctions.First();

        if (matchingFunction.ShadowsReference is null)
            throw new Exception($"Found a functions to resolve \"shadowed-by\" reference \"{reference.Name}\" but it is missing the \"shadowes\" attribute. This probably means there is an error in the gir file. Function is not going to be shadowed by another funciton.");

        if (matchingFunction.ShadowsReference.Name != reference.GetParentCallable().Name)
            throw new Exception($"Found a functions to resolve \"shadowed-by\" reference \"{reference.Name}\" but it has the wrong content in the \"shadows\" attribute. Expected: {reference.GetParentCallable().Name}, Found: {matchingFunction.ShadowsReference.Name}. This probably means there is an error in the gir file.");

        reference.Resolve(matchingFunctions.First());
    }
}
