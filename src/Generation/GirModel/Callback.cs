namespace GirModel;

public interface Callback : ComplexType, Callable
{
    new string Name { get; }
    bool Introspectable { get; }
}
