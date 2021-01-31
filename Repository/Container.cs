using Repository.Graph;
using Repository.Services;
using StrongInject;

namespace Repository
{
    [Register(typeof(Loader))]
    [Register(typeof(XmlService), typeof(IXmlService))]
    [Register(typeof(NamespaceInfoConverterService), typeof(INamespaceInfoConverterService))]
    [Register(typeof(TypeReferenceFactory), typeof(ITypeReferenceFactory))]
    [Register(typeof(InfoFactory), typeof(IInfoFactory))]
    public partial class Container : IContainer<Loader>
    {
        [Factory]
        public static IResolver<T> GetResolver<T>() where T : INode<T> => new Resolver<T>();

    }
}
