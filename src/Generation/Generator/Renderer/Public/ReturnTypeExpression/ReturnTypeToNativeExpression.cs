using System.Collections.Generic;

namespace Generator.Renderer.Public;

internal static class ReturnTypeToNativeExpression
{
    private static readonly List<ReturnTypeExpression.ReturnTypeConverter> Converter = new()
    {
        new ReturnTypeExpression.ToNative.Pointer(),
        new ReturnTypeExpression.ToNative.Bitfield(),
        new ReturnTypeExpression.ToNative.Enumeration(),
        new ReturnTypeExpression.ToNative.PrimitiveValueType(),
        new ReturnTypeExpression.ToNative.Record(),
        new ReturnTypeExpression.ToNative.String(),
    };

    public static string Render(GirModel.ReturnType from, string fromVariableName)
    {
        foreach (var converter in Converter)
            if (converter.Supports(from.AnyType))
                return converter.GetString(from, fromVariableName);

        throw new System.NotImplementedException($"Missing converter to convert from internal return type {from} to native.");
    }
}
