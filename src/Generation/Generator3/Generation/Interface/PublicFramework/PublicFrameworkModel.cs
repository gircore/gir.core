using Generator3.Converter;

namespace Generator3.Generation.Interface
{
    public class PublicFrameworkModel
    {
        private readonly GirModel.Interface _interface;

        public string Name => _interface.Name;
        public string NamespaceName => _interface.Namespace.GetPublicName();

        public PublicFrameworkModel(GirModel.Interface @interface)
        {
            _interface = @interface;
        }
    }
}
