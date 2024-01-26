using Generator.Model;

namespace Generator.Generator.Internal;

internal class UntypedRecord : Generator<GirModel.Record>
{
    private readonly Publisher _publisher;

    public UntypedRecord(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Record obj)
    {
        if (!Record.IsUntyped(obj))
            return;

        var source = Renderer.Internal.UntypedRecord.Render(obj);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(obj.Namespace),
            Name: obj.Name,
            Source: source,
            IsInternal: true
        );

        _publisher.Publish(codeUnit);
    }
}
