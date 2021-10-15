namespace Generator3.Model
{
    public class NativeRecordStructUnit
    {
        private readonly GirModel.Record _record;

        public string Name => _record.Name;
        public string NamespaceName => _record.NamespaceName + ".Native";

        public NativeRecordStructUnit(GirModel.Record record)
        {
            _record = record;
        }
    }
}
