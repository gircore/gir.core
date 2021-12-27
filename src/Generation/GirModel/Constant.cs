namespace GirModel
{
    public interface Constant
    {
        public Namespace Namespace { get; }
        string Name { get; }
        string Value { get; }
        Type Type { get; }
        bool Introspectable { get; }
    }
}
