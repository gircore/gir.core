namespace GirLoader.Output
{
    public partial class Union : GirModel.Union
    {
        GirModel.Namespace GirModel.ComplexType.Namespace => Repository.Namespace;
        string GirModel.ComplexType.Name => Name;
        GirModel.Method? GirModel.Union.TypeFunction => GetTypeFunction;
    }
}
