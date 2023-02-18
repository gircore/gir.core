using System.Collections.Generic;

namespace Generator.Renderer.Internal;

internal static class ReturnTypeToNativeExpression
{
    private static readonly List<ReturnTypeToNativeExpressions.ReturnTypeConverter> Converter = new()
    {
        new ReturnTypeToNativeExpressions.Pointer(),
        new ReturnTypeToNativeExpressions.Bitfield(),
        new ReturnTypeToNativeExpressions.Enumeration(),
        new ReturnTypeToNativeExpressions.PrimitiveValueType(),
        new ReturnTypeToNativeExpressions.Record(),
        new ReturnTypeToNativeExpressions.Utf8String(),
    };

    public static string Render(GirModel.ReturnType from, string fromVariableName)
    {
        foreach (var converter in Converter)
            if (converter.Supports(from.AnyType))
                return converter.GetString(from, fromVariableName);

        throw new System.NotImplementedException($"Missing converter to convert from internal return type {from} to native.");
    }
}
