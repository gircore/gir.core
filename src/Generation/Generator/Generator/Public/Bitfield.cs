using Generator.Model;

namespace Generator.Generator.Public;

internal class Bitfield : Generator<GirModel.Bitfield>
{
    private readonly Publisher _publisher;

    public Bitfield(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Bitfield obj)
    {
        var source = Renderer.Public.Bitfield.Render(obj);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(obj.Namespace),
            Name: obj.Name,
            Source: source,
            IsInternal: false
        );

        _publisher.Publish(codeUnit);
    }
}
