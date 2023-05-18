namespace GirModel;

public interface Callback : ComplexType, Callable
{
    new string Name { get; }
    ReturnType ReturnType { get; }
    bool Introspectable { get; }
}
