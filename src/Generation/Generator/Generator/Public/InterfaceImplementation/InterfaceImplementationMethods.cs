using System.Linq;
using Generator.Model;

namespace Generator.Generator.Public;

internal class InterfaceImplementationMethods(Publisher publisher) : Generator<GirModel.Interface>
{
    public void Generate(GirModel.Interface obj)
    {
        if (!obj.Methods.Where(Method.IsEnabled).Any())
            return;

        var source = Renderer.Public.InterfaceImplementationMethods.Render(obj);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(obj.Namespace),
            Name: $"{Model.Interface.GetImplementationName(obj)}.Methods",
            Source: source,
            IsInternal: false
        );

        publisher.Publish(codeUnit);
    }
}
