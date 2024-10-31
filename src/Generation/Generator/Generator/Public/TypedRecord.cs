using Generator.Model;

namespace Generator.Generator.Public;

internal class TypedRecord(Publisher publisher) : Generator<GirModel.Record>
{
    public void Generate(GirModel.Record record)
    {
        if (!Record.IsTyped(record))
            return;

        if (!Type.IsEnabled(record))
            return;

        var source = Renderer.Public.TypedRecord.Render(record);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(record.Namespace),
            Name: Record.GetPublicClassName(record),
            Source: source,
            IsInternal: false
        );

        publisher.Publish(codeUnit);
    }
}
