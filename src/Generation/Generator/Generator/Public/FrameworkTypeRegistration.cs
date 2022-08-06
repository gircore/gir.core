using Generator.Model;
using Generator.Renderer;

namespace Generator.Generator.Public;

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
            return;//We can not register any type of GLib as GLib is not using the GObject type system

        var source = Renderer.Public.FrameworkTypeRegistration.Render(ns);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(ns),
            Name: "Module.TypeRegistration",
            Source: source,
            IsInternal: false
        );

        _publisher.Publish(codeUnit);
    }
}
