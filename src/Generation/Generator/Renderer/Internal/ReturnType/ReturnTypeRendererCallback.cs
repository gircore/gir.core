using System.Collections.Generic;

namespace Generator.Renderer.Internal;

internal static class ReturnTypeRendererCallback
{
    private static readonly List<ReturnType.ReturnTypeConverter> converters = new()
    {
        new ReturnType.Bitfield(),
        new ReturnType.Class(),
        new ReturnType.ClassArray(),
        new ReturnType.Enumeration(),
        new ReturnType.ForeignTypedRecordCallback(),
        new ReturnType.Interface(),
        new ReturnType.InterfaceGLibPtrArray(),
        new ReturnType.OpaqueTypedRecordCallback(),
        new ReturnType.OpaqueUntypedRecordCallback(),
        new ReturnType.PlatformStringInCallback(),
        new ReturnType.PlatformStringArrayInCallback(),
        new ReturnType.Pointer(),
        new ReturnType.Long(),
        new ReturnType.UnsignedLong(),
        new ReturnType.PrimitiveValueType(),
        new ReturnType.PrimitiveValueTypeAlias(),
        new ReturnType.PrimitiveValueTypeArray(),
        new ReturnType.TypedRecordCallback(),
        new ReturnType.TypedRecordArray(),
        new ReturnType.Union(),
        new ReturnType.UntypedRecordCallback(),
        new ReturnType.Utf8StringInCallback(),
        new ReturnType.Utf8StringArrayInCallback(),
        new ReturnType.Void(),
    };

    public static string Render(GirModel.ReturnType returnType)
    {
        foreach (var converter in converters)
            if (converter.Supports(returnType))
                return converter.Convert(returnType).NullableTypeName;

        throw new System.Exception($"Internal return type for callback of type {returnType.AnyType} can not be rendered");
    }
}
