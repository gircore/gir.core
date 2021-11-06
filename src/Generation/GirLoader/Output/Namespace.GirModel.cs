namespace GirLoader.Output
{
    public partial class Namespace : GirModel.Namespace
    {
        string GirModel.Namespace.Name => Name;
        string GirModel.Namespace.Version => Version;
    }
}
