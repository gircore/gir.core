using Generator.Model;

namespace Generator.Generator.Internal;

internal class UnionStruct : Generator<GirModel.Union>
{
    private readonly Publisher _publisher;

    public UnionStruct(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Union union)
    {
        var source = Renderer.Internal.UnionStruct.Render(union);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(union.Namespace),
            Name: Union.GetInternalStructName(union),
            Source: source,
            IsInternal: true
        );

        _publisher.Publish(codeUnit);
    }
}
