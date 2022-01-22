namespace Generator3.Generation.Class.Standard
{
    public class PublicFrameworkModel
    {
        private readonly GirModel.Class _class;

        public string Name => _class.Name;
        public string NamespaceName => _class.Namespace.Name;
        public bool HasParent => _class.Parent is not null;
        public bool InheritsInitiallyUnowned => GetInheritsInitiallyUnowned(_class);
        public bool IsInitiallyUnowned => IsNamedInitiallyUnowned(_class.Name);

        public GirModel.Class? ParentClass => _class.Parent;

        public PublicFrameworkModel(GirModel.Class @class)
        {
            _class = @class;
        }

        private bool GetInheritsInitiallyUnowned(GirModel.Class @class)
        {
            if (@class.Parent is null)
                return false;

            return IsNamedInitiallyUnowned(@class.Parent.Name) || GetInheritsInitiallyUnowned(@class.Parent);
        }

        private static bool IsNamedInitiallyUnowned(string name) => name == "InitiallyUnowned";
    }
}
