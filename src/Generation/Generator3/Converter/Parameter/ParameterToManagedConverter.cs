using System;
using System.Collections.Generic;

namespace Generator3.Converter;

public static class ParameterToManagedConverter
{
    private static readonly List<ParameterConverter> Converter = new()
    {
        new Parameter.ToManaged.PrimitiveValueType(),
        new Parameter.ToManaged.Enumeration(),
        new Parameter.ToManaged.Bitfield(),
        new Parameter.ToManaged.Pointer(),
        new Parameter.ToManaged.String(),
        new Parameter.ToManaged.StringArray(),
        new Parameter.ToManaged.Record(),
        new Parameter.ToManaged.RecordArray(),
        new Parameter.ToManaged.Class(),
        new Parameter.ToManaged.Interface(),
    };

    public static string? ToManaged(this GirModel.Parameter from, out string variableName)
    {
        foreach (var converter in Converter)
            if (converter.Supports(from.AnyType))
                return converter.GetExpression(from, out variableName);

        throw new NotImplementedException($"Missing converter to convert from parameter {from.Name} ({from.AnyType}) to managed");
    }
}
