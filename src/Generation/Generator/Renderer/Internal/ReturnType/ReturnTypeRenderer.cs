using System.Collections.Generic;

namespace Generator.Renderer.Internal;

internal static class ReturnTypeRenderer
{
    private static readonly List<ReturnType.ReturnTypeConverter> converters = new()
    {
        new ReturnType.Bitfield(),
        new ReturnType.Class(),
        new ReturnType.ClassArray(),
        new ReturnType.Enumeration(),
        new ReturnType.ForeignTypedRecord(),
        new ReturnType.Interface(),
        new ReturnType.InterfaceGLibPtrArray(),
        new ReturnType.OpaqueTypedRecord(),
        new ReturnType.OpaqueUntypedRecord(),
        new ReturnType.PlatformString(),
        new ReturnType.PlatformStringArray(),
        new ReturnType.Pointer(),
        new ReturnType.PointerAlias(),
        new ReturnType.Long(), //Must be before primitive value type
        new ReturnType.UnsignedLong(), //Must be before primitive value type
        new ReturnType.PrimitiveValueType(),
        new ReturnType.PrimitiveValueTypeAlias(),
        new ReturnType.PrimitiveValueTypeAliasArray(),
        new ReturnType.PrimitiveValueTypeArray(),
        new ReturnType.TypedRecord(),
        new ReturnType.TypedRecordArray(),
        new ReturnType.Union(),
        new ReturnType.UntypedRecord(),
        new ReturnType.Utf8String(),
        new ReturnType.Utf8StringArray(),
        new ReturnType.Void(),
    };

    public static string Render(GirModel.ReturnType returnType)
    {
        foreach (var converter in converters)
            if (converter.Supports(returnType))
                return converter.Convert(returnType).NullableTypeName;

        throw new System.Exception($"Internal return type of type {returnType.AnyType} can not be rendered");
    }
}
