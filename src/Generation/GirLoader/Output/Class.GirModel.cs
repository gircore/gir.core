namespace GirLoader.Output
{
    public partial class Class : GirModel.Class
    {
        GirModel.Namespace GirModel.ComplexType.Namespace => Repository.Namespace; 
        string GirModel.ComplexType.Name => OriginalName;
        GirModel.Method GirModel.Class.TypeFunction => GetTypeFunction;
        bool GirModel.Class.Fundamental => IsFundamental;
    }
}
