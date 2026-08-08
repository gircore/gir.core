using System.Linq;
using GirLoader.Output;

namespace GirLoader;

internal static class InterfacesResolver
{
    public static void ResolveInterfaces(this RepositoryTypeReferenceResolver resolver, Repository repository)
    {
        foreach (var iface in repository.Namespace.Interfaces)
        {
            resolver.ResolveTypeReferences(iface.Implements.Select(AnyTypeReference.From), repository);
            resolver.ResolveTypeReferences(iface.Prerequisites.Select(AnyTypeReference.From), repository);
            resolver.ResolveTypeReferences(iface.Properties.Select(x => x.AnyTypeReference), repository);

            resolver.ResolveTypeReference(iface.GetTypeFunction.ReturnValue.AnyTypeReference, repository);
            resolver.ResolveParameterList(iface.GetTypeFunction.ParameterList, repository);

            resolver.ResolveTypeReferences(iface.Methods.Select(x => x.ReturnValue.AnyTypeReference), repository);
            resolver.ResolveParameterLists(iface.Methods.Select(x => x.ParameterList), repository);

            resolver.ResolveTypeReferences(iface.Functions.Select(x => x.ReturnValue.AnyTypeReference), repository);
            resolver.ResolveParameterLists(iface.Functions.Select(x => x.ParameterList), repository);

            resolver.ResolveTypeReferences(iface.Signals.Select(x => x.ReturnValue.AnyTypeReference), repository);
            resolver.ResolveParameterLists(iface.Signals.Select(x => x.ParameterList), repository);
        }

    }
}
