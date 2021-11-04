namespace Generator3.Generation.Record
{
    public class ClassModel
    {
        private readonly GirModel.Record _record;

        public string Name => _record.Name;
        public string NamespaceName => _record.NamespaceName;

        public ClassModel(GirModel.Record record)
        {
            _record = record;
        }
    }
}
