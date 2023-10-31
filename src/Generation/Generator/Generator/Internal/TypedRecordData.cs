using Generator.Model;

namespace Generator.Generator.Internal;

internal class TypedRecordData : Generator<GirModel.Record>
{
    private readonly Publisher _publisher;

    public TypedRecordData(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Record obj)
    {
        if (!Record.IsTyped(obj))
            return;

        var source = Renderer.Internal.TypedRecordData.Render(obj);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(obj.Namespace),
            Name: Model.TypedRecord.GetDataName(obj),
            Source: source,
            IsInternal: true
        );

        _publisher.Publish(codeUnit);
    }
}
