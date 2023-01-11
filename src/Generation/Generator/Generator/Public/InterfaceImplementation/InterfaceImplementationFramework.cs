using Generator.Model;

namespace Generator.Generator.Public;

internal class InterfaceImplementationFramework : Generator<GirModel.Interface>
{
    private readonly Publisher _publisher;

    public InterfaceImplementationFramework(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Interface obj)
    {
        //There is always at least the type function (GetGType) available

        var source = Renderer.Public.InterfaceImplementationFramework.Render(obj);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(obj.Namespace),
            Name: $"{Interface.GetImplementationName(obj)}.Framework",
            Source: source,
            IsInternal: false
        );

        _publisher.Publish(codeUnit);
    }
}
