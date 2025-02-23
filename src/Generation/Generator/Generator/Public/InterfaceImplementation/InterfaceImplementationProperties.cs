using System.Linq;
using Generator.Model;

namespace Generator.Generator.Public;

internal class InterfaceImplementationProperties : Generator<GirModel.Interface>
{
    private readonly Publisher _publisher;

    public InterfaceImplementationProperties(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Interface obj)
    {
        if (!obj.Properties.Where(Property.IsEnabled).Any())
            return;

        var source = Renderer.Public.InterfaceImplementationProperties.Render(obj);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(obj.Namespace),
            Name: $"{Interface.GetImplementationName(obj)}.Properties",
            Source: source,
            IsInternal: false
        );

        _publisher.Publish(codeUnit);
    }
}
