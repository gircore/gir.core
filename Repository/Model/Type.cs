namespace Repository.Model
{
    public interface TransferableType : Type
    {
        Transfer Transfer { get; }
    }
    public interface Type
    {
        SymbolReference SymbolReference { get; }
        TypeInformation TypeInformation { get; }
    }
}
