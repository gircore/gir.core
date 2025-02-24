using System.Collections.Generic;
using Generator.Model;

namespace Generator.Renderer.Public;

internal static class ConstantRenderer
{
    private static readonly List<Constant.ConstantsConverter> converters = new()
    {
        new Constant.Bitfield(),
        new Constant.PrimitiveValueType(),
        new Constant.PrimitiveValueTypeAlias(),
        new Constant.String(),
    };

    public static Constant.RenderableConstant Render(GirModel.Constant constant)
    {
        foreach (var converter in converters)
            if (converter.Supports(constant.Type))
                return converter.Convert(constant);

        throw new System.Exception($"Public constant \"{constant.Name}\" of type {constant.Type} and value {constant.Value} can not be rendered because a converter is missing.");
    }
}

