using System.Linq;
using Generator.Model;

namespace Generator.Generator.Public;

internal class ClassConstructOnlyProperties : Generator<GirModel.Class>
{
    private readonly Publisher _publisher;

    public ClassConstructOnlyProperties(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Class obj)
    {
        if (obj.Fundamental)
            return;

        if (obj.Properties.All(x => !x.ConstructOnly))
            return;

        var source = Renderer.Public.ClassConstructOnlyProperties.Render(obj);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(obj.Namespace),
            Name: $"{obj.Name}.ConstructOnlyProperties",
            Source: source,
            IsInternal: false
        );

        _publisher.Publish(codeUnit);
    }
}
