namespace GirLoader.Output
{
    public partial class Bitfield : GirModel.Bitfield
    {
        string GirModel.ComplexType.NamespaceName => Repository.Namespace.Name;
        string GirModel.ComplexType.Name => Name;
        GirModel.Method? GirModel.Bitfield.TypeFunction => null; //TODO: Should be implemented
    }
}
