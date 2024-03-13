using System.Collections.Generic;

namespace Generator.Renderer.Public;

internal static class Fields
{
    private static readonly List<Field.FieldConverter> Converters = new()
    {
        new Field.Bitfield(),
        new Field.Enumeration(),
        new Field.PrimitiveValueType(),
        new Field.String(),
    };

    public static Field.RenderableField GetRenderableField(GirModel.Field field)
    {
        foreach (var converter in Converters)
            if (converter.Supports(field))
                return converter.Convert(field);

        throw new System.Exception($"Public field \"{field.Name}\" of type {field.AnyTypeOrCallback} can not be rendered");
    }
}
