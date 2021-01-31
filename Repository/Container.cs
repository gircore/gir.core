using Repository.Factories;
using Repository.Graph;
using Repository.Services;
using StrongInject;

namespace Repository
{
    [Register(typeof(Loader))]
    [Register(typeof(XmlService), typeof(IXmlService))]
    [Register(typeof(NamespaceFactory), typeof(INamespaceFactory))]
    [Register(typeof(TypeReferenceFactory), typeof(ITypeReferenceFactory))]
    [Register(typeof(InfoFactory), typeof(IInfoFactory))]
    [Register(typeof(ClassFactory), typeof(IClassFactory))]
    [Register(typeof(AliasFactory), typeof(IAliasFactory))]
    [Register(typeof(ReturnValueFactory), typeof(IReturnValueFactory))]
    [Register(typeof(ArgumentFactory), typeof(IArgumentFactory))]
    [Register(typeof(ArgumentsFactory), typeof(IArgumentFactory))]
    [Register(typeof(CallbackFactory), typeof(ICallbackFactory))]
    public partial class Container : IContainer<Loader>
    {
        [Factory]
        public static IResolver<T> GetResolver<T>() where T : INode<T> => new Resolver<T>();

    }
}
