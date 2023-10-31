using Generator.Model;

namespace Generator.Generator.Public;

internal class TypedRecord : Generator<GirModel.Record>
{
    private readonly Publisher _publisher;

    public TypedRecord(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Record record)
    {
        if (!Record.IsTyped(record))
            return;

        var source = Renderer.Public.TypedRecord.Render(record);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(record.Namespace),
            Name: Record.GetPublicClassName(record),
            Source: source,
            IsInternal: false
        );

        _publisher.Publish(codeUnit);
    }
}
