using Generator.Model;

namespace Generator.Generator.Internal;

internal class ForeignTypedRecord : Generator<GirModel.Record>
{
    private readonly Publisher _publisher;

    public ForeignTypedRecord(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Record obj)
    {
        if (!Record.IsForeignTyped(obj))
            return;

        var source = Renderer.Internal.ForeignTypedRecord.Render(obj);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(obj.Namespace),
            Name: obj.Name,
            Source: source,
            IsInternal: true
        );

        _publisher.Publish(codeUnit);
    }
}
