using Generator3.Converter;

namespace Generator3.Generation.Record
{
    public class PublicClassModel
    {
        private readonly GirModel.Record _record;

        public string Name => _record.GetPublicClassName();
        public string NamespaceName => _record.Namespace.Name;

        public PublicClassModel(GirModel.Record record)
        {
            _record = record;
        }
    }
}
