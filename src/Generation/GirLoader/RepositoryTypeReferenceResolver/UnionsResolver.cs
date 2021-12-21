using System.Linq;
using GirLoader.Output;

namespace GirLoader
{
    internal static class UnionsResolver
    {
        public static void ResolveUnions(this RepositoryTypeReferenceResolver resolver, Repository repository)
        {
            foreach (var union in repository.Namespace.Unions)
            {
                resolver.ResolveTypeReferences(union.Fields.Select(x => x.TypeReference), repository);
                resolver.ResolveParameterLists(union.Fields.Select(x => x.Callback?.ParameterList).OfType<ParameterList>(), repository);
                resolver.ResolveTypeReferences(union.Fields.Select(x => x.Callback?.ReturnValue.TypeReference).OfType<TypeReference>(), repository);

                resolver.ResolveTypeReferences(union.Constructors.Select(x => x.ReturnValue.TypeReference), repository);
                resolver.ResolveParameterLists(union.Constructors.Select(x => x.ParameterList), repository);

                resolver.ResolveTypeReferences(union.Methods.Select(x => x.ReturnValue.TypeReference), repository);
                resolver.ResolveParameterLists(union.Methods.Select(x => x.ParameterList), repository);

                resolver.ResolveTypeReferences(union.Functions.Select(x => x.ReturnValue.TypeReference), repository);
                resolver.ResolveParameterLists(union.Functions.Select(x => x.ParameterList), repository);

                if (union.GetTypeFunction is not null)
                {
                    resolver.ResolveTypeReference(union.GetTypeFunction.ReturnValue.TypeReference, repository);
                    resolver.ResolveParameterList(union.GetTypeFunction.ParameterList, repository);
                }
            }
        }
    }
}
