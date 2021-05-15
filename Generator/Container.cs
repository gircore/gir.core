using Generator.Factories;
using Generator.Services;
using Generator.Services.Writer;
using StrongInject;

namespace Gir
{
    [Register(typeof(WriterService))]
    [Register(typeof(TemplateReaderService))]
    [Register(typeof(DllImportResolverFactory))]
    [Register(typeof(WriteHelperService))]
    [Register(typeof(WriteModuleService))]
    [Register(typeof(WriteSymbolsService))]
    [Register(typeof(WriteInterfaceService))]
    [Register(typeof(WriteClassService))]
    [Register(typeof(WriteElementsService))]
    [Register(typeof(WriteStaticService))]
    [Register(typeof(WriteRecordsService))]
    [Register(typeof(WriteSafeHandlesService))]
    [Register(typeof(WriteUnionsService))]
    [Register(typeof(WriteCallbacksService))]
    [Register(typeof(ScriptObjectFactory))]
    internal partial class Container : IContainer<WriterService>
    {
    }
}
