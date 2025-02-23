using Generator.Model;

namespace Generator.Generator.Internal;

internal class FundamentalClassMethods : Generator<GirModel.Class>
{
    private readonly Publisher _publisher;

    public FundamentalClassMethods(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Class obj)
    {
        if (!obj.Fundamental)
            return;

        var source = Renderer.Internal.FundamentalClassMethods.Render(obj);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(obj.Namespace),
            Name: $"{obj.Name}.Instance.Methods",
            Source: source,
            IsInternal: true
        );

        _publisher.Publish(codeUnit);
    }
}
