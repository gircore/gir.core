namespace GirLoader.Output
{
    public interface Parameter : Transferable, AnyType
    {
        SymbolName Name { get; }
        Direction Direction { get; }
        bool Nullable { get; }
        bool CallerAllocates { get; }
    }
}
