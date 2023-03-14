using System.Collections.Generic;

namespace Generator.Renderer.Public;

internal static class ParameterRenderer
{
    private static readonly List<Parameter.ParameterConverter> converters = new()
    {
        new Parameter.Bitfield(),
        new Parameter.Callback(),
        new Parameter.Class(),
        new Parameter.ClassArray(),
        new Parameter.Enumeration(),
        new Parameter.Interface(),
        new Parameter.InterfaceArray(),
        new Parameter.Pointer(),
        new Parameter.PointerAlias(),
        new Parameter.PrimitiveValueType(),
        new Parameter.PrimitiveValueTypeAlias(),
        new Parameter.PrimitiveValueTypeArray(),
        new Parameter.PrimitiveValueTypeArrayAlias(),
        new Parameter.PrimitiveValueTypeGLibArrayAlias(),
        new Parameter.Record(),
        new Parameter.RecordArray(),
        new Parameter.String(),
        new Parameter.StringArray(),
        new Parameter.Union(),
        new Parameter.Void(),
    };

    public static ParameterTypeData Render(GirModel.Parameter parameter)
    {
        if (parameter.AnyTypeOrVarArgs.IsT1)
            throw new System.Exception($"Public parameter \"{parameter.Name}\" of type {parameter.AnyTypeOrVarArgs} can not be rendered as variadic parameters are not supported");

        foreach (var converter in converters)
            if (converter.Supports(parameter.AnyTypeOrVarArgs.AsT0))
                return converter.Create(parameter);

        throw new System.Exception($"Public parameter \"{parameter.Name}\" of type {parameter.AnyTypeOrVarArgs} can not be rendered");
    }
}
