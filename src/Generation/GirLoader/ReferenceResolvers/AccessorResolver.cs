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
        property.Getter?.ResolveMethod(accessorProvider.Methods.First(method => method.Name == property.Getter.Name));

        // Workaround: Use "FirstOrDefault" because not all defined setters can be found e.g. if the
        // refrenced method uses varargs arguments (GTK4.Actionable.action-target property). Restore
        // ".First" and remove nullable argument from "ResolveMethod" if problem is fixed upstream
        property.Setter?.ResolveMethod(accessorProvider.Methods.FirstOrDefault(method => method.Name == property.Setter.Name));
    }

    private static void ResolvePropertyReference(Method method, AccessorProvider accessorProvider)
    {
        method.GetProperty?.ResolveProperty(accessorProvider.Properties.First(property => property.Name == method.GetProperty.Name));
        method.SetProperty?.ResolveProperty(accessorProvider.Properties.First(property => property.Name == method.SetProperty.Name));
    }
}
