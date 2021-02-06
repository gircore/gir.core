using Repository.Factories;
using Repository.Graph;
using Repository.Services;
using StrongInject;

namespace Repository
{
    [Register(typeof(RepositoryInternal))]
    [Register(typeof(XmlService), typeof(IXmlService))]
    [Register(typeof(NamespaceFactory), typeof(INamespaceFactory))]
    [Register(typeof(SymbolReferenceFactory), typeof(ISymbolReferenceFactory))]
    [Register(typeof(InfoFactory), typeof(IInfoFactory))]
    [Register(typeof(ClassFactory), typeof(IClassFactory))]
    [Register(typeof(AliasFactory), typeof(IAliasFactory))]
    [Register(typeof(ReturnValueFactory), typeof(IReturnValueFactory))]
    [Register(typeof(ArgumentFactory), typeof(IArgumentFactory))]
    [Register(typeof(ArgumentsFactory), typeof(IArgumentsFactory))]
    [Register(typeof(CallbackFactory), typeof(ICallbackFactory))]
    [Register(typeof(EnumerationFactory), typeof(IEnumartionFactory))]
    [Register(typeof(InterfaceFactory), typeof(IInterfaceFactory))]
    [Register(typeof(RecordFactory), typeof(IRecordFactory))]
    [Register(typeof(MethodFactory), typeof(IMethodFactory))]
    [Register(typeof(LoaderService), typeof(ILoaderService))]
    [Register(typeof(TypeReferenceResolverService), typeof(ITypeReferenceResolverService))]
    [Register(typeof(TransferFactory), typeof(ITransferFactory))]
    [Register(typeof(MemberFactory), typeof(IMemberFactory))]
    [Register(typeof(PascalCaseConverter), typeof(IPascalCaseConverter))]
    [Register(typeof(IdentifierConverter), typeof(IIdentifierConverter))]
    public partial class Container : IContainer<RepositoryInternal>
    {
        [Factory]
        public static IDependencyResolverService<T> GetResolver<T>() where T : INode<T> => new DependencyResolverService<T>();

    }
}
