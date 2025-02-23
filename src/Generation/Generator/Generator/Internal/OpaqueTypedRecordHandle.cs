using Generator.Model;

namespace Generator.Generator.Internal;

internal class OpaqueTypedRecordHandle : Generator<GirModel.Record>
{
    private readonly Publisher _publisher;

    public OpaqueTypedRecordHandle(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Record obj)
    {
        if (!Record.IsOpaqueTyped(obj))
            return;

        var source = Renderer.Internal.OpaqueTypedRecordHandle.Render(obj);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(obj.Namespace),
            Name: Model.OpaqueTypedRecord.GetInternalHandle(obj),
            Source: source,
            IsInternal: true
        );

        _publisher.Publish(codeUnit);
    }
}
