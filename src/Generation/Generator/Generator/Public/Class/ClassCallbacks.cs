using System.Linq;
using Generator.Model;

namespace Generator.Generator.Public;

internal class ClassCallbacks : Generator<GirModel.Class>
{
    private readonly Publisher _publisher;

    public ClassCallbacks(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Class obj)
    {
        if (obj.Fundamental)
            return;

        if (!obj.Callbacks.Any())
            return;

        var source = Renderer.Public.ClassCallbacks.Render(obj);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(obj.Namespace),
            Name: $"{obj.Name}.Callbacks",
            Source: source,
            IsInternal: false
        );

        _publisher.Publish(codeUnit);
    }
}
