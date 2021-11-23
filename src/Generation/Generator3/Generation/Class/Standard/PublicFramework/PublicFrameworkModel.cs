namespace Generator3.Generation.Class.Standard
{
    public class PublicFrameworkModel
    {
        private readonly GirModel.Class _class;

        public string Name => _class.Name;
        public string NamespaceName => _class.Namespace.Name;
        public bool HasParent => _class.Parent is not null;

        public GirModel.Class? ParentClass => _class.Parent;

        public PublicFrameworkModel(GirModel.Class @class)
        {
            _class = @class;
        }
    }
}
