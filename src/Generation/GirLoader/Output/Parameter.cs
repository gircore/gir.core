namespace GirLoader.Output;

public interface Parameter
{
    string Name { get; }
    Direction Direction { get; }
    bool Nullable { get; }
    bool CallerAllocates { get; }
}
