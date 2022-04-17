using Generator3.Converter;

namespace Generator3.Generation.Record
{
    public class InternalManagedHandleModel
    {
        private readonly GirModel.Record _record;

        public string Name => _record.GetInternalManagedHandleName();
        public string BaseHandle => _record.GetInternalHandleName();
        public string InternalStruct => _record.GetInternalStructName();
        public string NamespaceName => _record.Namespace.GetInternalName();
        public GirModel.PlatformDependent? PlatformDependent => _record as GirModel.PlatformDependent;

        public InternalManagedHandleModel(GirModel.Record record)
        {
            _record = record;
        }
    }
}
