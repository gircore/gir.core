namespace Generator3.Generation.Union
{
    public class InternalStructModel
    {
        private readonly GirModel.Union _union;

        public string Name => _union.Name;
        public string NamespaceName => _union.Namespace.GetInternalName();

        public InternalStructModel(GirModel.Union union)
        {
            _union = union;
        }
    }
}
