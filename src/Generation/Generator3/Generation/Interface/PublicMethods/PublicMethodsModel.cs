namespace Generator3.Generation.Interface
{
    public class PublicMethodsModel
    {
        private readonly GirModel.Interface _interface;

        public string Name => _interface.Name;
        public string NamespaceName => _interface.Namespace.Name;

        public PublicMethodsModel(GirModel.Interface @interface)
        {
            _interface = @interface;
        }
    }
}
