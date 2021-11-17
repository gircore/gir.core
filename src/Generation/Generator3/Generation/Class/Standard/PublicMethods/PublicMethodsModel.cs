namespace Generator3.Generation.Class.Standard
{
    public class PublicMethodsModel
    {
        private readonly GirModel.Class _class;

        public string Name => _class.Name;
        public string NamespaceName => _class.Namespace.GetInternalName();

        public PublicMethodsModel(GirModel.Class @class)
        {
            _class = @class;
        }
    }
}
