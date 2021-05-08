namespace Repository.Model
{
    public interface Parameter : TransferableType
    {
        SymbolName SymbolName { get; }
        Direction Direction { get; }
        bool Nullable { get; }
        bool CallerAllocates { get; }
    }
}
