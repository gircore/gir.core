using Generator.Model;
using Generator.Renderer;

namespace Generator.Generator.Internal;

internal class RecordManagedHandle : Generator<GirModel.Record>
{
    private readonly Publisher _publisher;

    public RecordManagedHandle(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Record record)
    {
        var source = Renderer.Internal.RecordManagedHandle.Render(record);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(record.Namespace),
            Name: Record.GetInternalManagedHandleName(record),
            Source: source,
            IsInternal: true
        );

        _publisher.Publish(codeUnit);
    }
}
