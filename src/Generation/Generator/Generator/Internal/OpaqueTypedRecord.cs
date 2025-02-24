using Generator.Model;

namespace Generator.Generator.Internal;

internal class OpaqueTypedRecord : Generator<GirModel.Record>
{
    private readonly Publisher _publisher;

    public OpaqueTypedRecord(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Record obj)
    {
        if (!Record.IsOpaqueTyped(obj))
            return;

        var source = Renderer.Internal.OpaqueTypedRecord.Render(obj);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(obj.Namespace),
            Name: obj.Name,
            Source: source,
            IsInternal: true
        );

        _publisher.Publish(codeUnit);
    }
}
