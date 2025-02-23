using System.Linq;
using Generator.Model;

namespace Generator.Generator.Public;

internal class ClassProperties : Generator<GirModel.Class>
{
    private readonly Publisher _publisher;

    public ClassProperties(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Class obj)
    {
        if (obj.Fundamental)
            return;

        if (!obj.Properties.Any())
            return;

        var source = Renderer.Public.ClassProperties.Render(obj);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(obj.Namespace),
            Name: $"{obj.Name}.Properties",
            Source: source,
            IsInternal: false
        );

        _publisher.Publish(codeUnit);
    }
}
