using System;
using System.Collections.Generic;

namespace Generator3.Converter;

public static class ParameterToNativeConverter
{
    private static readonly List<ParameterConverter> Converter = new()
    {
        new Parameter.ToNative.PrimitiveValueType(),
        new Parameter.ToNative.Enumeration(),
        new Parameter.ToNative.Bitfield(),
        new Parameter.ToNative.String(),
        new Parameter.ToNative.StringArray(),
        new Parameter.ToNative.Interface(),
        new Parameter.ToNative.InterfaceArray(),
        new Parameter.ToNative.Class(),
        new Parameter.ToNative.ClassArray(),
        new Parameter.ToNative.Record(),
        new Parameter.ToNative.RecordArray(),
    };

    public static string? ToNative(this GirModel.Parameter from, out string variableName)
    {
        foreach (var converter in Converter)
            if (converter.Supports(from.AnyType))
                return converter.GetExpression(from, out variableName);

        throw new NotImplementedException($"Missing converter to convert from parameter {from.Name} ({from.AnyType}) to native");
    }
}
