using Generator.Model;

namespace Generator.Generator.Public;

internal class InterfaceProperties : Generator<GirModel.Interface>
{
    private readonly Publisher _publisher;

    public InterfaceProperties(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Interface obj)
    {
        var source = Renderer.Public.InterfaceProperties.Render(obj);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(obj.Namespace),
            Name: $"{obj.Name}.Properties",
            Source: source,
            IsInternal: false
        );

        _publisher.Publish(codeUnit);
    }
}
