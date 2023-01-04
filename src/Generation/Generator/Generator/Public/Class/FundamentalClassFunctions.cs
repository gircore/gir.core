using System.Linq;
using Generator.Model;

namespace Generator.Generator.Public;

internal class FundamentalClassFunctions : Generator<GirModel.Class>
{
    private readonly Publisher _publisher;

    public FundamentalClassFunctions(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Class obj)
    {
        if (!obj.Fundamental)
            return;

        if (!obj.Functions.Any())
            return;

        var source = Renderer.Public.FundamentalClassFunctions.Render(obj);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(obj.Namespace),
            Name: $"{obj.Name}.Functions",
            Source: source,
            IsInternal: false
        );

        _publisher.Publish(codeUnit);
    }
}
