namespace GirLoader.Output;

public interface Callable : GirModel.Callable
{
    new string Name { get; }
    ShadowsReference? ShadowsReference { get; }
    ShadowedByReference? ShadowedByReference { get; }
}
