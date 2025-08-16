using System.Linq;
using Generator.Model;

namespace Generator.Generator.Public;

internal class ClassInterfaceSignals(Publisher publisher) : Generator<GirModel.Class>
{
    public void Generate(GirModel.Class obj)
    {
        if (obj.Fundamental)
            return;

        if (!obj.Implements.SelectMany(x => x.Signals).Any())
            return;

        var source = Renderer.Public.ClassInterfaceSignals.Render(obj);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(obj.Namespace),
            Name: $"{obj.Name}.InterfaceSignals",
            Source: source,
            IsInternal: false
        );

        publisher.Publish(codeUnit);
    }
}
