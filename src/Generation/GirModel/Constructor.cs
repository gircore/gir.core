namespace GirModel;

public interface Constructor : Callable
{
    ComplexType Parent { get; }
    string CIdentifier { get; }
    string? Version { get; }
}
