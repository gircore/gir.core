using System.Linq;
using GirLoader.Output;

namespace GirLoader
{
    internal static class ClassesResolver
    {
        public static void ResolveClasses(this RepositoryTypeReferenceResolver resolver, Repository repository)
        {
            foreach (var cls in repository.Namespace.Classes)
            {
                if(cls.Parent is not null)
                    resolver.ResolveTypeReference(cls.Parent, repository);
                
                resolver.ResolveTypeReferences(cls.Implements, repository);
                resolver.ResolveTypeReferences(cls.Properties.Select(x => x.TypeReference), repository);
                
                resolver.ResolveTypeReferences(cls.Fields.Select(x => x.TypeReference), repository);
                resolver.ResolveParameterLists(cls.Fields.Select(x => x.Callback?.ParameterList).OfType<ParameterList>(), repository);
                resolver.ResolveTypeReferences(cls.Fields.Select(x => x.Callback?.ReturnValue.TypeReference).OfType<TypeReference>(), repository);

                resolver.ResolveTypeReference(cls.GetTypeFunction.ReturnValue.TypeReference, repository);
                resolver.ResolveParameterList(cls.GetTypeFunction.ParameterList, repository);

                resolver.ResolveTypeReferences(cls.Constructors.Select(x => x.ReturnValue.TypeReference), repository);
                resolver.ResolveParameterLists(cls.Constructors.Select(x => x.ParameterList), repository);

                resolver.ResolveTypeReferences(cls.Methods.Select(x => x.ReturnValue.TypeReference), repository);
                resolver.ResolveParameterLists(cls.Methods.Select(x => x.ParameterList), repository);

                resolver.ResolveTypeReferences(cls.Functions.Select(x => x.ReturnValue.TypeReference), repository);
                resolver.ResolveParameterLists(cls.Functions.Select(x => x.ParameterList), repository);

                resolver.ResolveTypeReferences(cls.Signals.Select(x => x.ReturnValue.TypeReference), repository);
                resolver.ResolveParameterLists(cls.Signals.Select(x => x.ParameterList), repository);
            }
        }
    }
}
