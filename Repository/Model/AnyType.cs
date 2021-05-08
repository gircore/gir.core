namespace Repository.Model
{
    public interface TransferableAnyType : AnyType
    {
        Transfer Transfer { get; }
    }
    public interface AnyType
    {
        SymbolReference SymbolReference { get; }
        TypeInformation TypeInformation { get; }
    }
}
