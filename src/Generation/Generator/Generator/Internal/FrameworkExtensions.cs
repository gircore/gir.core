using Generator.Model;
using Generator.Renderer;

namespace Generator.Generator.Internal;

internal class FrameworkExtensions : Generator<GirModel.Namespace>
{
    private readonly Publisher _publisher;

    public FrameworkExtensions(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Namespace ns)
    {
        var source = Renderer.Internal.FrameworkExtensions.Render(ns);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(ns),
            Name: "Extensions",
            Source: source,
            IsInternal: true
        );

        _publisher.Publish(codeUnit);
    }
}
