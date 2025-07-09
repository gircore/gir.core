using Generator.Model;

namespace Generator.Generator.Public;

internal class Interface(Publisher publisher) : Generator<GirModel.Interface>
{
    public void Generate(GirModel.Interface obj)
    {
        var source = Renderer.Public.Interface.Render(obj);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(obj.Namespace),
            Name: obj.Name,
            Source: source,
            IsInternal: false
        );

        publisher.Publish(codeUnit);
    }
}
