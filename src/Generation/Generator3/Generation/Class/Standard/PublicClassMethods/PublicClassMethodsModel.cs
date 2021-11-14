namespace Generator3.Generation.Class.Standard
{
    public class PublicClassMethodsModel
    {
        private readonly GirModel.Class _class;

        public string Name => _class.Name;
        public string NamespaceName => _class.Namespace.GetNativeName();

        public PublicClassMethodsModel(GirModel.Class @class)
        {
            _class = @class;
        }
    }
}
