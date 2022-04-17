using Generator3.Converter;

namespace Generator3.Generation.Record
{
    public class PublicClassModel
    {
        private readonly GirModel.Record _record;

        public string Name => _record.GetPublicClassName();
        public string InternalHandleName => _record.GetInternalHandleName();
        public string InternalOwnedHandleName => _record.GetInternalOwnedHandleName();
        public string InternalUnownedHandleName => _record.GetInternalUnownedHandleName();
        public string NamespaceName => _record.Namespace.GetPublicName();
        public GirModel.PlatformDependent? PlatformDependent => _record as GirModel.PlatformDependent;

        public PublicClassModel(GirModel.Record record)
        {
            _record = record;
        }
    }
}
