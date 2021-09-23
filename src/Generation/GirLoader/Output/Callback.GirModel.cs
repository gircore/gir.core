namespace GirLoader.Output
{
    public partial class Callback : GirModel.Callback
    {
        string GirModel.ComplexType.NamespaceName => Repository.Namespace.Name;
        string GirModel.ComplexType.Name => Name;
    }
}
