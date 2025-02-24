using System;
using Generator.Model;

namespace Generator.Generator.Public;

internal class AliasPrimitiveValueType : Generator<GirModel.Alias>
{
    private readonly Publisher _publisher;

    public AliasPrimitiveValueType(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(GirModel.Alias obj)
    {
        try
        {
            if (obj.Type is not GirModel.PrimitiveValueType)
                return;

            if (!Alias.IsEnabled(obj))
                return;

            var source = Renderer.Public.AliasPrimitiveValueType.Render(obj);
            var codeUnit = new CodeUnit(
                Project: Model.Namespace.GetCanonicalName(obj.Namespace),
                Name: obj.Name,
                Source: source,
                IsInternal: false
            );

            _publisher.Publish(codeUnit);
        }
        catch (Exception ex)
        {
            Log.Warning($"Could not render primitive value type Alias {obj.Name}: {ex.Message}");
        }
    }
}
