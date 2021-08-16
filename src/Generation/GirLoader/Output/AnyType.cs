namespace GirLoader.Output
{
    public interface TransferableAnyType : AnyType
    {
        Transfer Transfer { get; }
    }

    public interface AnyType
    {
        TypeReference TypeReference { get; }
    }
}
