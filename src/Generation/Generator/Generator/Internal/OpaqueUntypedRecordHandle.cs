using Generator.Model;

namespace Generator.Generator.Internal;

internal class OpaqueUntypedRecordHandle : Generator<GirModel.Record>
{
    private readonly Publisher _publisher;

    public OpaqueUntypedRecordHandle(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Record obj)
    {
        if (!Record.IsOpaqueUntyped(obj))
            return;

        var source = Renderer.Internal.OpaqueUntypedRecordHandle.Render(obj);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(obj.Namespace),
            Name: Model.OpaqueTypedRecord.GetInternalHandle(obj),
            Source: source,
            IsInternal: true
        );

        _publisher.Publish(codeUnit);
    }
}
