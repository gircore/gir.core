namespace GirModel;

public interface Constructor : Callable
{
    ComplexType Parent { get; }
    string CIdentifier { get; }
    ReturnType ReturnType { get; }
    string? Version { get; }
}
