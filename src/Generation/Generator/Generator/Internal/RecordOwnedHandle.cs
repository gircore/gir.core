using Generator.Model;

namespace Generator.Generator.Internal;

internal class RecordOwnedHandle : Generator<GirModel.Record>
{
    private readonly Publisher _publisher;

    public RecordOwnedHandle(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Record record)
    {
        if (!Record.IsStandard(record))
            return;

        var source = Renderer.Internal.RecordOwnedHandle.Render(record);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(record.Namespace),
            Name: Record.GetInternalOwnedHandleName(record),
            Source: source,
            IsInternal: true
        );

        _publisher.Publish(codeUnit);
    }
}
