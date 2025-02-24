using System.Collections.Generic;

namespace Generator.Renderer.Public;

internal static class ReturnTypeToManagedExpression
{
    private static readonly List<ReturnTypeToManagedExpressions.ReturnTypeConverter> Converter =
    [
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
        new ReturnTypeToManagedExpressions.Long(), //Must be before primitive value type
        new ReturnTypeToManagedExpressions.UnsignedLong(), //Must be before primitive value type
        new ReturnTypeToManagedExpressions.CLong(), //Must be before primitive value type
        new ReturnTypeToManagedExpressions.UnsignedCLong(), //Must be before primitive value type
        new ReturnTypeToManagedExpressions.PrimitiveValueType(),
        new ReturnTypeToManagedExpressions.PrimitiveValueTypeAlias(),
        new ReturnTypeToManagedExpressions.PrimitiveValueTypeAliasArray(),
        new ReturnTypeToManagedExpressions.PrimitiveValueTypeArray(),
        new ReturnTypeToManagedExpressions.TypedRecord(),
        new ReturnTypeToManagedExpressions.UntypedRecord(),
        new ReturnTypeToManagedExpressions.Utf8String(),
        new ReturnTypeToManagedExpressions.Utf8StringArray(),
        new ReturnTypeToManagedExpressions.Void()
    ];

    public static ReturnTypeToManagedData Initialize(GirModel.ReturnType returnType, IEnumerable<ParameterToNativeData> parameters)
    {
        var data = new ReturnTypeToManagedData(returnType);

        foreach (var converter in Converter)
        {
            if (!converter.Supports(returnType.AnyType))
                continue;

            converter.Initialize(data, parameters);
            return data;
        }

        throw new System.NotImplementedException($"Missing converter to convert from internal return type {returnType} to public.");
    }
}
