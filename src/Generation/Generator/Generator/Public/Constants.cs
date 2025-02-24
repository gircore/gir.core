using System.Collections.Generic;
using System.Linq;
using Generator.Model;

namespace Generator.Generator.Public;

internal class Constants : Generator<IEnumerable<GirModel.Constant>>
{
    private readonly Publisher _publisher;

    public Constants(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(IEnumerable<GirModel.Constant> obj)
    {
        if (!obj.Any())
            return;

        var project = Namespace.GetCanonicalName(obj.First().Namespace);

        var codeUnit = obj
            .Map(Renderer.Public.ConstantsRenderer.Render)
            .Map(source => new CodeUnit(project, "Constants", source, false));

        _publisher.Publish(codeUnit);
    }
}
