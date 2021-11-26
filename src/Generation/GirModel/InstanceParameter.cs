namespace GirModel
{
    public interface InstanceParameter
    {
        string Name { get; }
        Direction Direction { get; }
        Transfer Transfer { get; }
        bool Nullable { get; }
        bool CallerAllocates { get; }
        Type Type { get; }
    }
}
