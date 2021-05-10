namespace Repository.Model
{
    public interface TransferableAnyType : AnyType
    {
        Transfer Transfer { get; }
    }
    public interface AnyType
    {
        TypeReference TypeReference { get; }
        TypeInformation TypeInformation { get; }
    }
}
