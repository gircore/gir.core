using Generator.Model;

namespace Generator.Generator.Internal;

internal class UntypedRecordHandle : Generator<GirModel.Record>
{
    private readonly Publisher _publisher;

    public UntypedRecordHandle(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Record obj)
    {
        if (!Record.IsUntyped(obj))
            return;

        var source = Renderer.Internal.UntypedRecordHandle.Render(obj);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(obj.Namespace),
            Name: Model.UntypedRecord.GetInternalHandle(obj),
            Source: source,
            IsInternal: true
        );

        _publisher.Publish(codeUnit);
    }
}
