using System.Linq;
using Generator.Model;

namespace Generator.Generator.Internal;

internal class UnionMethods : Generator<GirModel.Union>
{
    private readonly Publisher _publisher;

    public UnionMethods(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Union union)
    {
        if (!union.Constructors.Any()
            && !union.Functions.Any()
            && !union.Methods.Any()
            && union.TypeFunction is null)
            return;

        var source = Renderer.Internal.UnionMethods.Render(union);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(union.Namespace),
            Name: union.Name,
            Source: source,
            IsInternal: true
        );

        _publisher.Publish(codeUnit);
    }
}
