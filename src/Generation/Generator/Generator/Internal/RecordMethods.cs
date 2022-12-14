using System.Linq;
using Generator.Model;

namespace Generator.Generator.Internal;

internal class RecordMethods : Generator<GirModel.Record>
{
    private readonly Publisher _publisher;

    public RecordMethods(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Record record)
    {
        if (!record.Constructors.Any()
            && !record.Methods.Any()
            && !record.Functions.Any()
            && record.TypeFunction is null)
            return;


        var source = Renderer.Internal.RecordMethods.Render(record);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(record.Namespace),
            Name: record.Name,
            Source: source,
            IsInternal: true
        );

        _publisher.Publish(codeUnit);
    }
}
