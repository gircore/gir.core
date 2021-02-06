using Generator.Factories;
using Generator.Services;
using Generator.Services.Writer;
using StrongInject;

namespace Repository
{
    [Register(typeof(WriterService))]
    [Register(typeof(TemplateReaderService), typeof(ITemplateReaderService))]
    [Register(typeof(DllImportResolverFactory), typeof(IDllImportResolverFactory))]
    [Register(typeof(WriteHelperService), typeof(IWriteHelperService))]
    [Register(typeof(WriteDllImportService), typeof(IWriteDllImportService))]
    [Register(typeof(WriteTypesService), typeof(IWriteTypesService))]
    public partial class Container : IContainer<WriterService>
    {
    }
}
