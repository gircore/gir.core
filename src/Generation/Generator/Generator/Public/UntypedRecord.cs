using Generator.Model;

namespace Generator.Generator.Public;

internal class UntypedRecord : Generator<GirModel.Record>
{
    private readonly Publisher _publisher;

    public UntypedRecord(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Record record)
    {
        if (!Record.IsUntyped(record))
            return;

        var source = Renderer.Public.UntypedRecord.Render(record);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(record.Namespace),
            Name: Record.GetPublicClassName(record),
            Source: source,
            IsInternal: false
        );

        _publisher.Publish(codeUnit);
    }
}
