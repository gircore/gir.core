using Generator.Model;

namespace Generator.Generator.Internal;

internal class TypedRecordData(Publisher publisher) : Generator<GirModel.Record>
{
    public void Generate(GirModel.Record obj)
    {
        if (!Record.IsTyped(obj))
            return;

        if (!Type.IsEnabled(obj))
            return;

        var source = Renderer.Internal.TypedRecordData.Render(obj);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(obj.Namespace),
            Name: Model.TypedRecord.GetDataName(obj),
            Source: source,
            IsInternal: true
        );

        publisher.Publish(codeUnit);
    }
}
