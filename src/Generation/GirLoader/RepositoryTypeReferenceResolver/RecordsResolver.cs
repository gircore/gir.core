using System.Linq;
using GirLoader.Output;

namespace GirLoader
{
    internal static class RecordsResolver
    {
        public static void ResolveRecords(this RepositoryTypeReferenceResolver resolver, Repository repository)
        {
            foreach (var record in repository.Namespace.Records)
            {
                resolver.ResolveTypeReferences(record.Fields.Select(x => x.TypeReference), repository);
                resolver.ResolveParameterLists(record.Fields.Select(x => x.Callback?.ParameterList).OfType<ParameterList>(), repository);
                resolver.ResolveTypeReferences(record.Fields.Select(x => x.Callback?.ReturnValue.TypeReference).OfType<TypeReference>(), repository);
                
                resolver.ResolveTypeReferences(record.Constructors.Select(x => x.ReturnValue.TypeReference), repository);
                resolver.ResolveParameterLists(record.Constructors.Select(x => x.ParameterList), repository);
                
                resolver.ResolveTypeReferences(record.Methods.Select(x => x.ReturnValue.TypeReference), repository);
                resolver.ResolveParameterLists(record.Methods.Select(x => x.ParameterList), repository);
                
                resolver.ResolveTypeReferences(record.Functions.Select(x => x.ReturnValue.TypeReference), repository);
                resolver.ResolveParameterLists(record.Functions.Select(x => x.ParameterList), repository);

                if (record.GetTypeFunction is not null)
                {
                    resolver.ResolveTypeReference(record.GetTypeFunction.ReturnValue.TypeReference, repository);
                    resolver.ResolveParameterList(record.GetTypeFunction.ParameterList, repository);
                }

                if (record.GLibClassStructFor is not null)
                    resolver.ResolveTypeReference(record.GLibClassStructFor, repository);
            }
        }
    }
}
