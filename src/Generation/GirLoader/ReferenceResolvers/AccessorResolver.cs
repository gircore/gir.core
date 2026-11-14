using System.Collections.Generic;
using System.Linq;
using GirLoader.Output;

namespace GirLoader;

internal static class AccessorResolver
{
    public static void ResolveAccessors(this IEnumerable<AccessorProvider> accessorProviders)
    {
        foreach (var accessorProvider in accessorProviders)
            ResolveAccessors(accessorProvider);
    }

    private static void ResolveAccessors(AccessorProvider accessorProvider)
    {
        foreach (var method in accessorProvider.Methods)
            ResolvePropertyReference(method, accessorProvider);

        foreach (var property in accessorProvider.Properties)
            ResolveMethodReference(property, accessorProvider);
    }

    private static void ResolveMethodReference(Property property, AccessorProvider accessorProvider)
    {
        try
        {
            property.Getter?.ResolveMethod(accessorProvider.Methods.First(method => method.Name == property.Getter.Name));
        }
        catch
        {
            property.Getter = null;
            Log.Error($"Could not resolve getter method for property {property.Name}.");
        }

        try
        {
            property.Setter?.ResolveMethod(accessorProvider.Methods.First(method => method.Name == property.Setter.Name));
        }
        catch
        {
            property.Setter = null;
            Log.Error($"Could not resolve setter method for property {property.Name}.");
        }
    }

    private static void ResolvePropertyReference(Method method, AccessorProvider accessorProvider)
    {
        try
        {
            method.GetProperty?.ResolveProperty(accessorProvider.Properties.First(property => property.Name == method.GetProperty.Name));
        }
        catch
        {
            method.GetProperty = null;
            Log.Error($"Could not resolve getter property for method {method.Name}.");
        }

        try
        {
            method.SetProperty?.ResolveProperty(accessorProvider.Properties.First(property => property.Name == method.SetProperty.Name));
        }
        catch
        {
            method.SetProperty = null;
            Log.Error($"Could not resolve setter property for method {method.Name}.");
        }
    }
}
