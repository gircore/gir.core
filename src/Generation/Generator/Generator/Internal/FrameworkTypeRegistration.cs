using Generator.Model;

namespace Generator.Generator.Internal;

internal class FrameworkTypeRegistration : Generator<GirModel.Namespace>
{
    private readonly Publisher _publisher;

    public FrameworkTypeRegistration(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Namespace ns)
    {
        if (ns.Name == "GLib")
            return;//We can not register any type of GLib as the type system is defined inside GObject

        var source = Renderer.Internal.FrameworkTypeRegistration.Render(ns);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(ns),
            Name: "TypeRegistration",
            Source: source,
            IsInternal: true
        );

        _publisher.Publish(codeUnit);
    }
}
