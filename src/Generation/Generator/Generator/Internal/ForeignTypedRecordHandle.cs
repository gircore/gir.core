using Generator.Model;

namespace Generator.Generator.Internal;

internal class ForeignTypedRecordHandle : Generator<GirModel.Record>
{
    private readonly Publisher _publisher;

    public ForeignTypedRecordHandle(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Record obj)
    {
        if (!Record.IsForeignTyped(obj))
            return;

        var source = Renderer.Internal.ForeignTypedRecordHandle.Render(obj);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(obj.Namespace),
            Name: Model.ForeignTypedRecord.GetInternalHandle(obj),
            Source: source,
            IsInternal: true
        );

        _publisher.Publish(codeUnit);
    }
}
