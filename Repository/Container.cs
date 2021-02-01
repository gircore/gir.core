using Repository.Factories;
using Repository.Graph;
using Repository.Services;
using StrongInject;

namespace Repository
{
    [Register(typeof(RepositoryInternal))]
    [Register(typeof(XmlService), typeof(IXmlService))]
    [Register(typeof(NamespaceFactory), typeof(INamespaceFactory))]
    [Register(typeof(TypeReferenceFactory), typeof(ITypeReferenceFactory))]
    [Register(typeof(InfoFactory), typeof(IInfoFactory))]
    [Register(typeof(ClassFactory), typeof(IClassFactory))]
    [Register(typeof(AliasFactory), typeof(IAliasFactory))]
    [Register(typeof(ReturnValueFactory), typeof(IReturnValueFactory))]
    [Register(typeof(ArgumentFactory), typeof(IArgumentFactory))]
    [Register(typeof(ArgumentsFactory), typeof(IArgumentsFactory))]
    [Register(typeof(CallbackFactory), typeof(ICallbackFactory))]
    [Register(typeof(EnumartionFactory), typeof(IEnumartionFactory))]
    [Register(typeof(InterfaceFactory), typeof(IInterfaceFactory))]
    [Register(typeof(RecordFactory), typeof(IRecordFactory))]
    [Register(typeof(MethodFactory), typeof(IMethodFactory))]
    [Register(typeof(LoaderService), typeof(ILoaderService))]
    public partial class Container : IContainer<RepositoryInternal>
    {
        [Factory]
        public static IResolver<T> GetResolver<T>() where T : INode<T> => new Resolver<T>();

    }
}
