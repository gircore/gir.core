using Repository.Services;
using StrongInject;

namespace Repository
{
    [Register(typeof(Parser))]
    [Register(typeof(XmlService), typeof(IXmlService))]
    public partial class Container : IContainer<Parser> {}
}
