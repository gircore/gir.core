namespace GirModel
{
    public interface InstanceParameter : Nullable
    {
        string Name { get; }
        Direction Direction { get; }
        Transfer Transfer { get; }
        bool CallerAllocates { get; }
        Type Type { get; }
    }
}
