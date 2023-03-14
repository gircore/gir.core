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
        new ReturnType.Interface(),
        new ReturnType.PlatformString(),
        new ReturnType.Pointer(),
        new ReturnType.PrimitiveValueType(),
        new ReturnType.PrimitiveValueTypeAlias(),
        new ReturnType.PrimitiveValueTypeArray(),
        new ReturnType.Record(),
        new ReturnType.RecordArray(),
        new ReturnType.StringArray(),
        new ReturnType.Union(),
        new ReturnType.Utf8String(),
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
