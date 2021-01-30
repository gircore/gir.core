using Repository.Graph;
using Repository.Services;
using StrongInject;

namespace Repository
{
    [Register(typeof(Parser))]
    [Register(typeof(XmlService), typeof(IXmlService))]
    [Register(typeof(NamespaceInfoConverterService), typeof(INamespaceInfoConverterService))]
    [Register(typeof(TypeReferenceFactory), typeof(ITypeReferenceFactory))]
    [Register(typeof(Resolver), Scope.InstancePerDependency, typeof(IResolver))]
    public partial class Container : IContainer<Parser> {}
}
