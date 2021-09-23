namespace GirModel
{
    public interface Parameter
    {
        public string Name { get; }
        public Type Type { get; }
        Direction Direction { get; }
        Transfer Transfer { get; }
        bool Nullable { get; }
        bool CallerAllocates { get; }
    }
}
