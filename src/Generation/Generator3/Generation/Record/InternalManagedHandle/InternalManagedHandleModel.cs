using Generator3.Converter;

namespace Generator3.Generation.Record
{
    public class InternalManagedHandleModel
    {
        private readonly GirModel.Record _record;

        public string Name => _record.Name;
        public string HandleClassName => "ManagedHandle";
        public string BaseHandle => "Handle";
        public string InternalStruct => _record.GetInternalStructName();
        public string NamespaceName => _record.Namespace.GetInternalName();

        public InternalManagedHandleModel(GirModel.Record record)
        {
            _record = record;
        }
    }
}
