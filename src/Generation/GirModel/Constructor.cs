namespace GirModel;

public interface Constructor : Callable
{
    string CIdentifier { get; }
    ReturnType ReturnType { get; }
    string? Version { get; }
}
