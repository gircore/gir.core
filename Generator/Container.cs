using Generator.Factories;
using Generator.Services;
using Generator.Services.Writer;
using StrongInject;

namespace Repository
{
    [Register(typeof(WriterService))]
    [Register(typeof(TemplateReaderService))]
    [Register(typeof(DllImportResolverFactory))]
    [Register(typeof(WriteHelperService))]
    [Register(typeof(WriteDllImportService))]
    [Register(typeof(WriteTypesService))]
    [Register(typeof(WriteSymbolsService))]
    internal partial class Container : IContainer<WriterService>
    {
    }
}
