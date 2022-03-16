namespace GirModel
{
    public interface ComplexType : Type
    {
        public Namespace Namespace { get; }
        public string Name { get; }
    }
}
