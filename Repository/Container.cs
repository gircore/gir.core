using Repository.Factories;
using Repository.Graph;
using Repository.Services;
using StrongInject;

namespace Repository
{
    [Register(typeof(RepositoryInternal))]
    [Register(typeof(XmlService))]
    [Register(typeof(NamespaceFactory))]
    [Register(typeof(SymbolReferenceFactory))]
    [Register(typeof(InfoFactory))]
    [Register(typeof(ClassFactory))]
    [Register(typeof(AliasFactory))]
    [Register(typeof(ReturnValueFactory))]
    [Register(typeof(ArgumentFactory))]
    [Register(typeof(ArgumentsFactory))]
    [Register(typeof(CallbackFactory))]
    [Register(typeof(EnumerationFactory))]
    [Register(typeof(InterfaceFactory))]
    [Register(typeof(RecordFactory))]
    [Register(typeof(MethodFactory))]
    [Register(typeof(LoaderService))]
    [Register(typeof(TypeReferenceResolverService))]
    [Register(typeof(ClassStructResolverService))]
    [Register(typeof(TransferFactory))]
    [Register(typeof(MemberFactory))]
    [Register(typeof(CaseConverter))]
    [Register(typeof(IdentifierConverter))]
    [Register(typeof(PropertyFactory))]
    [Register(typeof(FieldFactory))]
    [Register(typeof(SignalFactory))]
    [Register(typeof(ConstantFactory))]
    internal partial class Container : IContainer<RepositoryInternal>
    {
        [Factory]
        public static DependencyResolverService<T> GetResolver<T>() where T : INode<T> => new DependencyResolverService<T>();

    }
}
