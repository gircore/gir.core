namespace Generator3.Generation.Record
{
    public class InternalSafeHandleModel
    {
        private readonly GirModel.Record _record;

        public string Name => _record.Name;
        public string HandleClassName => "Handle";
        public string NamespaceName => _record.Namespace.GetInternalName();

        public InternalSafeHandleModel(GirModel.Record record)
        {
            _record = record;
        }
    }
}
