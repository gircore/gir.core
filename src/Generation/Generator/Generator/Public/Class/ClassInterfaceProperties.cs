using System.Linq;
using Generator.Model;

namespace Generator.Generator.Public;

internal class ClassInterfaceProperties : Generator<GirModel.Class>
{
    private readonly Publisher _publisher;

    public ClassInterfaceProperties(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Class obj)
    {
        if (obj.Fundamental)
            return;

        if (!obj.Implements.SelectMany(x => x.Properties).Any())
            return;

        var source = Renderer.Public.ClassInterfaceProperties.Render(obj);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(obj.Namespace),
            Name: $"{obj.Name}.InterfaceProperties",
            Source: source,
            IsInternal: false
        );

        _publisher.Publish(codeUnit);
    }
}
