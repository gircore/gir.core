using System.Collections.Generic;

namespace Generator3.Converter;

public static class ReturnTypeToNativeConverter
{
    private static readonly List<ReturnTypeConverter> Converter = new()
    {
        new ReturnType.ToNative.Pointer(),
        new ReturnType.ToNative.Bitfield(),
        new ReturnType.ToNative.Enumeration(),
        new ReturnType.ToNative.PrimitiveValueType(),
        new ReturnType.ToNative.Record(),
        new ReturnType.ToNative.String(),
    };

    public static string ToNative(this GirModel.ReturnType from, string fromVariableName)
    {
        foreach (var converter in Converter)
            if (converter.Supports(from.AnyType))
                return converter.GetString(from, fromVariableName);

        throw new System.NotImplementedException($"Missing converter to convert from internal return type {from} to native.");
    }
}
