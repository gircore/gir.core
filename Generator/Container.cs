using Generator;
using Generator.Services;
using StrongInject;

namespace Repository
{
    [Register(typeof(WriterService))]
    [Register(typeof(TemplateReaderService), typeof(ITemplateReaderService))]
    public partial class Container : IContainer<WriterService>
    {
    }
}
