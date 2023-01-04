using Generator.Model;

namespace Generator.Generator.Public;

internal class InterfaceImplementationFunctions : Generator<GirModel.Interface>
{
    private readonly Publisher _publisher;

    public InterfaceImplementationFunctions(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Interface obj)
    {
        var source = Renderer.Public.InterfaceImplementationFunctions.Render(obj);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(obj.Namespace),
            Name: $"{Interface.GetImplementationName(obj)}.Functions",
            Source: source,
            IsInternal: false
        );

        _publisher.Publish(codeUnit);
    }
}
