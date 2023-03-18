using System.Collections.Generic;

namespace Generator.Renderer.Internal;

internal static class ReturnTypeRenderer
{
    private static readonly List<ReturnType.ReturnTypeConverter> converters = new()
    {
        new ReturnType.PrimitiveValueType(),
        new ReturnType.Bitfield(),
        new ReturnType.Enumeration(),
        new ReturnType.Utf8String(),
        new ReturnType.PlatformString(),
        new ReturnType.Record(),
        new ReturnType.Class(),
        new ReturnType.Interface(),
        new ReturnType.Pointer(),
        new ReturnType.Union(),
        new ReturnType.Void(),

        new ReturnType.StringArray(),
        new ReturnType.RecordArray(),
        new ReturnType.ClassArray(),
        new ReturnType.PrimitiveValueTypeArray(),
    };

    public static string Render(GirModel.ReturnType returnType)
    {
        foreach (var converter in converters)
            if (converter.Supports(returnType))
                return converter.Convert(returnType).NullableTypeName;

        throw new System.Exception($"Return type of type {returnType.AnyType} can not be rendered");
    }
}
