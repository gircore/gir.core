using Repository.Services;
using StrongInject;

namespace Repository
{
    [Register(typeof(Parser))]
    [Register(typeof(XmlService), typeof(IXmlService))]
    [Register(typeof(NamespaceInfoConverterServiceService), typeof(INamespaceInfoConverterService))]
    [Register(typeof(TypeReferenceFactory), typeof(ITypeReferenceFactory))]
    public partial class Container : IContainer<Parser> {}
}
