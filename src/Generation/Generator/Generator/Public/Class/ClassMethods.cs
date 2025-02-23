using System.Linq;
using Generator.Model;

namespace Generator.Generator.Public;

internal class ClassMethods : Generator<GirModel.Class>
{
    private readonly Publisher _publisher;

    public ClassMethods(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Class obj)
    {
        if (obj.Fundamental)
            return;

        if (!obj.Methods.Any())
            return;

        var source = Renderer.Public.ClassMethods.Render(obj);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(obj.Namespace),
            Name: $"{obj.Name}.Methods",
            Source: source,
            IsInternal: false
        );

        _publisher.Publish(codeUnit);
    }
}
