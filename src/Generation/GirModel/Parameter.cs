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
        bool IsPointer { get; }
        bool IsConst { get; }
        bool IsVolatile { get; }
        AnyType AnyType { get; }
        Scope Scope { get; }
    }
}
