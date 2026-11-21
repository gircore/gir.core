using System.Linq;
using Generator.Model;

namespace Generator.Generator.Internal;

internal class TypedRecordDelegates(Publisher publisher) : Generator<GirModel.Record>
{
    public void Generate(GirModel.Record record)
    {
        if (!Record.IsTyped(record))
            return;

        if (!record.Fields.Any(field => field.AnyTypeReferenceOrCallback.IsCallback))
            return;

        if (!Type.IsEnabled(record))
            return;

        var source = Renderer.Internal.TypedRecordDelegates.Render(record);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(record.Namespace),
            Name: $"{Model.TypedRecord.GetDataName(record)}.Delegates",
            Source: source,
            IsInternal: true
        );

        publisher.Publish(codeUnit);
    }
}
