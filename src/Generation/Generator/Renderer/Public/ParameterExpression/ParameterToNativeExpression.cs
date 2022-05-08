using System;
using System.Collections.Generic;

namespace Generator.Renderer.Public;

internal static class ParameterToNativeExpression
{
    private static readonly List<ParameterExpressions.ParameterConverter> Converter = new()
    {
        new ParameterExpressions.ToNative.PrimitiveValueType(),
        new ParameterExpressions.ToNative.Enumeration(),
        new ParameterExpressions.ToNative.Bitfield(),
        new ParameterExpressions.ToNative.String(),
        new ParameterExpressions.ToNative.StringArray(),
        new ParameterExpressions.ToNative.Interface(),
        new ParameterExpressions.ToNative.InterfaceArray(),
        new ParameterExpressions.ToNative.Class(),
        new ParameterExpressions.ToNative.ClassArray(),
        new ParameterExpressions.ToNative.Record(),
        new ParameterExpressions.ToNative.RecordArray(),
    };

    public static string? ToNative(this GirModel.Parameter from, out string variableName)
    {
        foreach (var converter in Converter)
            if (converter.Supports(from.AnyType))
                return converter.GetExpression(from, out variableName);

        throw new NotImplementedException($"Missing converter to convert from parameter {from.Name} ({from.AnyType}) to native");
    }
}
