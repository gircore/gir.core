using Generator.Model;
using Generator.Renderer;

namespace Generator.Generator.Public;

internal class ClassConstructors : Generator<GirModel.Class>
{
    private readonly Publisher _publisher;

    public ClassConstructors(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Class obj)
    {
        if (obj.IsFundamental)
            return;

        var source = Renderer.Public.ClassConstructors.Render(obj);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(obj.Namespace),
            Name: $"{obj.Name}.Constructors",
            Source: source,
            IsInternal: false
        );

        _publisher.Publish(codeUnit);
    }
}
