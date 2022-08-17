namespace GirModel
{
    public interface ComplexType : Type
    {
        Namespace Namespace { get; }
        string Name { get; }
    }
}
