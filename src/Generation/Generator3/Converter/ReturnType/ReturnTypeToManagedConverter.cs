using System.Collections.Generic;

namespace Generator3.Converter;

public static class ReturnTypeToManagedConverter
{
    private static readonly List<ReturnTypeConverter> Converter = new()
    {
        new ReturnType.ToManaged.Pointer(),
        new ReturnType.ToManaged.Bitfield(),
        new ReturnType.ToManaged.Enumeration(),
        new ReturnType.ToManaged.PrimitiveValueType(),
        new ReturnType.ToManaged.PrimitiveValueTypeArray(),
        new ReturnType.ToManaged.Utf8String(),
        new ReturnType.ToManaged.StringArray(),
        new ReturnType.ToManaged.PlatformString(),
        new ReturnType.ToManaged.Class(),
        new ReturnType.ToManaged.Interface(),
        new ReturnType.ToManaged.Record(),
    };

    public static string ToManaged(this GirModel.ReturnType from, string fromVariableName)
    {
        foreach (var converter in Converter)
            if (converter.Supports(from.AnyType))
                return converter.GetString(from, fromVariableName);

        throw new System.NotImplementedException($"Missing converter to convert from internal return type {from} to public.");
    }
}
