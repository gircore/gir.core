using Generator.Model;

namespace Generator.Generator.Public;

internal class InterfaceEvents(Publisher publisher) : Generator<GirModel.Interface>
{
    public void Generate(GirModel.Interface obj)
    {
        var source = Renderer.Public.InterfaceEvents.Render(obj);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(obj.Namespace),
            Name: $"{obj.Name}.Events",
            Source: source,
            IsInternal: false
        );

        publisher.Publish(codeUnit);
    }
}
