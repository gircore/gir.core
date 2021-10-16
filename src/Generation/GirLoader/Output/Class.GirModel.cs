namespace GirLoader.Output
{
    public partial class Class : GirModel.Class
    {
        string GirModel.ComplexType.NamespaceName => Repository.Namespace.Name; 
        string GirModel.ComplexType.Name => OriginalName;
        GirModel.Method GirModel.Class.TypeFunction => GetTypeFunction;
        bool GirModel.Class.Fundamental => IsFundamental;
    }
}
