using Generator.Model;
using Generator.Renderer;

namespace Generator.Generator.Public;

internal class InterfaceMethods : Generator<GirModel.Interface>
{
    private readonly Publisher _publisher;

    public InterfaceMethods(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Interface obj)
    {
        var source = Renderer.Public.InterfaceMethods.Render(obj);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(obj.Namespace),
            Name: $"{obj.Name}.Methods",
            Source: source,
            IsInternal: false
        );

        _publisher.Publish(codeUnit);
    }
}
