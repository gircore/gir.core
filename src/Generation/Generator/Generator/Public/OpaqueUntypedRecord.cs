using Generator.Model;

namespace Generator.Generator.Public;

internal class OpaqueUntypedRecord : Generator<GirModel.Record>
{
    private readonly Publisher _publisher;

    public OpaqueUntypedRecord(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Record record)
    {
        if (!Record.IsOpaqueUntyped(record))
            return;

        var source = Renderer.Public.OpaqueUntypedRecord.Render(record);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(record.Namespace),
            Name: Record.GetPublicClassName(record),
            Source: source,
            IsInternal: false
        );

        _publisher.Publish(codeUnit);
    }
}
