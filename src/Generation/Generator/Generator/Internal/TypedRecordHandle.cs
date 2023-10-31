using Generator.Model;

namespace Generator.Generator.Internal;

internal class TypedRecordHandle : Generator<GirModel.Record>
{
    private readonly Publisher _publisher;

    public TypedRecordHandle(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Record obj)
    {
        if (!Record.IsTyped(obj))
            return;

        var source = Renderer.Internal.TypedRecordHandle.Render(obj);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(obj.Namespace),
            Name: Model.TypedRecord.GetInternalHandle(obj),
            Source: source,
            IsInternal: true
        );

        _publisher.Publish(codeUnit);
    }
}
