using Generator.Model;

namespace Generator.Generator.Internal;

internal class UntypedRecordData : Generator<GirModel.Record>
{
    private readonly Publisher _publisher;

    public UntypedRecordData(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Record obj)
    {
        if (!Record.IsUntyped(obj))
            return;

        var source = Renderer.Internal.UntypedRecordData.Render(obj);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(obj.Namespace),
            Name: Model.UntypedRecord.GetDataName(obj),
            Source: source,
            IsInternal: true
        );

        _publisher.Publish(codeUnit);
    }
}
