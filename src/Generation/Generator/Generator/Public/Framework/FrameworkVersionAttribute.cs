using Generator.Model;
using Generator.Renderer;

namespace Generator.Generator.Public;

internal class FrameworkVersionAttribute : Generator<GirModel.Namespace>
{
    private readonly Publisher _publisher;

    public FrameworkVersionAttribute(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Namespace ns)
    {
        var source = Renderer.Public.FrameworkVersionAttribute.Render(ns);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(ns),
            Name: "VersionAttribute",
            Source: source,
            IsInternal: false
        );

        _publisher.Publish(codeUnit);
    }
}
