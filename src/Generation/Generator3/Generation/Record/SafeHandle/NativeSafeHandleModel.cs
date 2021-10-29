namespace Generator3.Generation.Record
{
    public class NativeSafeHandleModel
    {
        private readonly GirModel.Record _record;

        public string Name => _record.Name;
        public string HandleClassName => "Handle";
        public string NamespaceName => _record.NamespaceName + ".Native";

        public NativeSafeHandleModel(GirModel.Record record)
        {
            _record = record;
        }
    }
}
