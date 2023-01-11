using System.Linq;
using Generator.Model;

namespace Generator.Generator.Public;

internal class ClassInterfaceMethods : Generator<GirModel.Class>
{
    private readonly Publisher _publisher;

    public ClassInterfaceMethods(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Class obj)
    {
        if (obj.Fundamental)
            return;

        if (!obj.Implements.SelectMany(x => x.Methods).Any())
            return;

        var source = Renderer.Public.ClassInterfaceMethods.Render(obj);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(obj.Namespace),
            Name: $"{obj.Name}.InterfaceMethods",
            Source: source,
            IsInternal: false
        );

        _publisher.Publish(codeUnit);
    }
}
