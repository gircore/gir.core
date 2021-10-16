namespace GirLoader.Output
{
    public partial class Union : GirModel.Union
    {
        string GirModel.ComplexType.NamespaceName => Repository.Namespace.Name;
        string GirModel.ComplexType.Name => Name;
        GirModel.Method? GirModel.Union.TypeFunction => GetTypeFunction;
    }
}
