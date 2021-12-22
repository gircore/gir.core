namespace GirModel
{
    public interface ReturnType
    {
        AnyType AnyType { get; }
        Transfer Transfer { get; }
        bool Nullable { get; }
        bool IsPointer { get; }
    }
}
