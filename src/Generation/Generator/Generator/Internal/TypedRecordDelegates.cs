using System.Linq;
using Generator.Model;

namespace Generator.Generator.Internal;

internal class TypedRecordDelegates : Generator<GirModel.Record>
{
    private readonly Publisher _publisher;

    public TypedRecordDelegates(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Record record)
    {
        if (!Record.IsTyped(record))
            return;

        if (!record.Fields.Any(field => field.AnyTypeOrCallback.IsT1))
            return;

        var source = Renderer.Internal.TypedRecordDelegates.Render(record);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(record.Namespace),
            Name: $"{Model.TypedRecord.GetDataName(record)}.Delegates",
            Source: source,
            IsInternal: true
        );

        _publisher.Publish(codeUnit);
    }
}
