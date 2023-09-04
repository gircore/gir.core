using System.Linq;
using Generator.Model;

namespace Generator.Generator.Internal;

internal class RecordDelegates : Generator<GirModel.Record>
{
    private readonly Publisher _publisher;

    public RecordDelegates(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Record record)
    {
        if (!Record.IsStandard(record))
            return;

        if (!record.Fields.Any(field => field.AnyTypeOrCallback.IsT1))
            return;

        var source = Renderer.Internal.RecordDelegates.Render(record);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(record.Namespace),
            Name: $"{Record.GetInternalStructName(record)}.Delegates",
            Source: source,
            IsInternal: true
        );

        _publisher.Publish(codeUnit);
    }
}
