using System.Collections.Generic;

namespace Generator.Renderer.Public;

internal static class ReturnTypeToManagedExpression
{
    private static readonly List<ReturnTypeToManagedExpressions.ReturnTypeConverter> Converter = new()
    {
        new ReturnTypeToManagedExpressions.Bitfield(),
        new ReturnTypeToManagedExpressions.Class(),
        new ReturnTypeToManagedExpressions.Enumeration(),
        new ReturnTypeToManagedExpressions.ForeignTypedRecord(),
        new ReturnTypeToManagedExpressions.Interface(),
        new ReturnTypeToManagedExpressions.OpaqueTypedRecord(),
        new ReturnTypeToManagedExpressions.OpaqueUntypedRecord(),
        new ReturnTypeToManagedExpressions.PlatformString(),
        new ReturnTypeToManagedExpressions.PlatformStringArray(),
        new ReturnTypeToManagedExpressions.Pointer(),
        new ReturnTypeToManagedExpressions.PointerAlias(),
        new ReturnTypeToManagedExpressions.PrimitiveValueType(),
        new ReturnTypeToManagedExpressions.PrimitiveValueTypeAlias(),
        new ReturnTypeToManagedExpressions.PrimitiveValueTypeArray(),
        new ReturnTypeToManagedExpressions.TypedRecord(),
        new ReturnTypeToManagedExpressions.UntypedRecord(),
        new ReturnTypeToManagedExpressions.Utf8String(),
        new ReturnTypeToManagedExpressions.Utf8StringArray(),
    };

    public static string Render(GirModel.ReturnType from, string fromVariableName)
    {
        foreach (var converter in Converter)
            if (converter.Supports(from.AnyType))
                return converter.GetString(from, fromVariableName);

        throw new System.NotImplementedException($"Missing converter to convert from internal return type {from} to public.");
    }
}
