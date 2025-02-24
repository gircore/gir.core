using Generator.Model;

namespace Generator.Generator.Internal;

internal class InterfaceMethods : Generator<GirModel.Interface>
{
    private readonly Publisher _publisher;

    public InterfaceMethods(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Interface obj)
    {
        var source = Renderer.Internal.InterfaceMethods.Render(obj);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(obj.Namespace),
            Name: obj.Name,
            Source: source,
            IsInternal: true
        );

        _publisher.Publish(codeUnit);
    }
}
