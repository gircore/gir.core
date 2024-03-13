using Generator.Model;

namespace Generator.Generator.Internal;

internal class ForeignUntypedRecordHandle : Generator<GirModel.Record>
{
    private readonly Publisher _publisher;

    public ForeignUntypedRecordHandle(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Record obj)
    {
        if (!Record.IsForeignUntyped(obj))
            return;

        var source = Renderer.Internal.ForeignUntypedRecordHandle.Render(obj);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(obj.Namespace),
            Name: Model.ForeignTypedRecord.GetInternalHandle(obj),
            Source: source,
            IsInternal: true
        );

        _publisher.Publish(codeUnit);
    }
}
