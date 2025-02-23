using System.Linq;
using Generator.Model;

namespace Generator.Generator.Public;

internal class InterfaceImplementationMethods : Generator<GirModel.Interface>
{
    private readonly Publisher _publisher;

    public InterfaceImplementationMethods(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Interface obj)
    {
        if (!obj.Methods.Where(Method.IsEnabled).Any())
            return;

        var source = Renderer.Public.InterfaceImplementationMethods.Render(obj);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(obj.Namespace),
            Name: $"{Interface.GetImplementationName(obj)}.Methods",
            Source: source,
            IsInternal: false
        );

        _publisher.Publish(codeUnit);
    }
}
