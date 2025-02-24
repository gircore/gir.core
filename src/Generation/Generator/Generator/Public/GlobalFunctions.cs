using System.Collections.Generic;
using System.Linq;
using Generator.Model;

namespace Generator.Generator.Public;

internal class GlobalFunctions : Generator<IEnumerable<GirModel.Function>>
{
    private readonly Publisher _publisher;

    public GlobalFunctions(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(IEnumerable<GirModel.Function> functions)
    {
        if (!functions.Any())
            return;

        var source = Renderer.Public.GlobalFunctions.Render(functions);
        var codeUnit = new CodeUnit(
            Project: Namespace.GetCanonicalName(functions.First().Namespace),
            Name: "Functions",
            Source: source,
            IsInternal: false
        );

        _publisher.Publish(codeUnit);
    }
}
