using Generator.Model;
using Generator.Renderer;

namespace Generator.Generator.Public;

internal class ClassSignals : Generator<GirModel.Class>
{
    private readonly Publisher _publisher;

    public ClassSignals(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Class obj)
    {
        if (obj.Fundamental)
            return;

        var source = Renderer.Public.ClassSignals.Render(obj);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(obj.Namespace),
            Name: $"{obj.Name}.Signals",
            Source: source,
            IsInternal: false
        );

        _publisher.Publish(codeUnit);
    }
}
