using Generator;
using Generator.Factories;
using Generator.Services;
using StrongInject;

namespace Repository
{
    [Register(typeof(WriterService))]
    [Register(typeof(TemplateReaderService), typeof(ITemplateReaderService))]
    [Register(typeof(DllImportResolverFactory), typeof(IDllImportResolverFactory))]
    public partial class Container : IContainer<WriterService>
    {
    }
}
