namespace GirModel
{
    public interface Parameter
    {
        string Name { get; }
        Direction Direction { get; }
        Transfer Transfer { get; }
        bool Nullable { get; }
        bool CallerAllocates { get; }
        int? Closure { get; }
        AnyTypeReference AnyTypeReference { get; }
        Scope Scope { get; }
    }
}
