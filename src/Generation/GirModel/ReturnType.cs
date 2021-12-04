namespace GirModel
{
    public interface ReturnType
    {
        AnyType AnyType { get; }
        Transfer Transfer { get; }
        bool Nullable { get; }
        bool IsPointer { get; }

        bool IsOwnedRef => Transfer switch
        {
            Transfer.None => false,
            Transfer.Full => true,
            Transfer.Container => true,
            _ => throw new System.Exception("Unknown transfer type")
        };
    }
}
