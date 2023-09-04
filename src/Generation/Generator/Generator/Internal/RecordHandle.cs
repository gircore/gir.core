using Generator.Model;

namespace Generator.Generator.Internal;

internal class RecordHandle : Generator<GirModel.Record>
{
    private readonly Publisher _publisher;

    public RecordHandle(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Record record)
    {
        if (!Record.IsStandard(record))
            return;

        var source = Renderer.Internal.RecordHandle.Render(record);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(record.Namespace),
            Name: Record.GetInternalHandleName(record),
            Source: source,
            IsInternal: true
        );

        _publisher.Publish(codeUnit);
    }
}
