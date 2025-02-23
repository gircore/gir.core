using System.Collections.Generic;

namespace Generator.Renderer.Internal;

internal static class ReturnTypeToNativeExpression
{
    private static readonly List<ReturnTypeToNativeExpressions.ReturnTypeConverter> Converter = new()
    {
        new ReturnTypeToNativeExpressions.Bitfield(),
        new ReturnTypeToNativeExpressions.Class(),
        new ReturnTypeToNativeExpressions.Enumeration(),
        new ReturnTypeToNativeExpressions.ForeignTypedRecord(),
        new ReturnTypeToNativeExpressions.Interface(),
        new ReturnTypeToNativeExpressions.OpaqueTypedRecord(),
        new ReturnTypeToNativeExpressions.OpaqueUntypedRecord(),
        new ReturnTypeToNativeExpressions.Pointer(),
        new ReturnTypeToNativeExpressions.Long(), //Must be before Primitive value type
        new ReturnTypeToNativeExpressions.UnsignedLong(), //Must be before Primitive value type
        new ReturnTypeToNativeExpressions.CLong(), //Must be before Primitive value type
        new ReturnTypeToNativeExpressions.UnsignedCLong(), //Must be before primitive value type
        new ReturnTypeToNativeExpressions.PrimitiveValueType(),
        new ReturnTypeToNativeExpressions.PrimitiveValueTypeAlias(),
        new ReturnTypeToNativeExpressions.TypedRecord(),
        new ReturnTypeToNativeExpressions.Utf8String(),
    };

    public static string Render(GirModel.ReturnType from, string fromVariableName)
    {
        foreach (var converter in Converter)
            if (converter.Supports(from.AnyType))
                return converter.GetString(from, fromVariableName);

        throw new System.NotImplementedException($"Missing converter to convert from internal return type {from.AnyType} to native.");
    }
}
