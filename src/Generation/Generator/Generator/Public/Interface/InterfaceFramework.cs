using Generator.Model;

namespace Generator.Generator.Public;

internal class InterfaceFramework : Generator<GirModel.Interface>
{
    private readonly Publisher _publisher;

    public InterfaceFramework(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Interface obj)
    {
        var source = Renderer.Public.InterfaceFramework.Render(obj);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(obj.Namespace),
            Name: $"{obj.Name}.Framework",
            Source: source,
            IsInternal: false
        );

        _publisher.Publish(codeUnit);
    }
}
