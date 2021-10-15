namespace GirLoader.Output
{
    public partial class Class : GirModel.Class
    {
        public string NamespaceName => Repository.Namespace.Name.Value; 
        public string Name => OriginalName.Value;
        GirModel.Method GirModel.Class.TypeFunction => GetTypeFunction;
        public bool Fundamental => IsFundamental;
    }
}
