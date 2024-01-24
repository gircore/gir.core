using Generator.Model;

namespace Generator.Generator.Public;

internal class ForeignTypedRecord : Generator<GirModel.Record>
{
    private readonly Publisher _publisher;

    public ForeignTypedRecord(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Record record)
    {
        if (!Record.IsForeignTyped(record))
            return;

        var source = Renderer.Public.ForeignTypedRecord.Render(record);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(record.Namespace),
            Name: Record.GetPublicClassName(record),
            Source: source,
            IsInternal: false
        );

        _publisher.Publish(codeUnit);
    }
}
