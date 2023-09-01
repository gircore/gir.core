using Generator.Model;

namespace Generator.Generator.Internal;

internal class RecordStruct : Generator<GirModel.Record>
{
    private readonly Publisher _publisher;

    public RecordStruct(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Record record)
    {
        if (Record.IsOpaqueTyped(record))
            return;

        var source = Renderer.Internal.RecordStruct.Render(record);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(record.Namespace),
            Name: Record.GetInternalStructName(record),
            Source: source,
            IsInternal: true
        );

        _publisher.Publish(codeUnit);
    }
}
