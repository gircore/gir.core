namespace Generator3.Model
{
    public class NativeRecordFunctionsUnit
    {
        private readonly GirModel.Record _record;

        public string Name => _record.Name;
        public string NamespaceName => _record.NamespaceName + ".Native";

        public NativeRecordFunctionsUnit(GirModel.Record @record)
        {
            _record = record;
        }
    }
}
