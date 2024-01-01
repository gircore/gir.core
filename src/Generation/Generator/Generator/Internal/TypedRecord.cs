using Generator.Model;

namespace Generator.Generator.Internal;

internal class TypedRecord : Generator<GirModel.Record>
{
    private readonly Publisher _publisher;

    public TypedRecord(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Record obj)
    {
        if (!Record.IsTyped(obj))
            return;

        var source = Renderer.Internal.TypedRecord.Render(obj);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(obj.Namespace),
            Name: obj.Name,
            Source: source,
            IsInternal: true
        );

        _publisher.Publish(codeUnit);
    }
}
