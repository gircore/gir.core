using Generator.Model;

namespace Generator.Generator.Public;

internal class OpaqueTypedRecord : Generator<GirModel.Record>
{
    private readonly Publisher _publisher;

    public OpaqueTypedRecord(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Record record)
    {
        if (!Record.IsOpaqueTyped(record))
            return;

        var source = Renderer.Public.OpaqueTypedRecord.Render(record);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(record.Namespace),
            Name: Record.GetPublicClassName(record),
            Source: source,
            IsInternal: false
        );

        _publisher.Publish(codeUnit);
    }
}
