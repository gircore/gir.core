namespace Generator3.Generation.Record
{
    public class NativeManagedHandleModel
    {
        private readonly GirModel.Record _record;

        public string Name => _record.Name;
        public string HandleClassName => "ManagedHandle";
        public string BaseHandle => "Handle";
        public string NativeStruct => $"{Name}.Struct";
        public string NamespaceName => _record.Namespace.GetNativeName();

        public NativeManagedHandleModel(GirModel.Record record)
        {
            _record = record;
        }
    }
}
