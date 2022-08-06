using System;
using System.Collections.Generic;

namespace Generator.Renderer.Public;

internal static class ParameterToManagedExpression
{
    private static readonly List<ParameterExpressions.ParameterConverter> Converter = new()
    {
        new ParameterExpressions.ToManaged.PrimitiveValueType(),
        new ParameterExpressions.ToManaged.Enumeration(),
        new ParameterExpressions.ToManaged.Bitfield(),
        new ParameterExpressions.ToManaged.Pointer(),
        new ParameterExpressions.ToManaged.String(),
        new ParameterExpressions.ToManaged.StringArray(),
        new ParameterExpressions.ToManaged.Record(),
        new ParameterExpressions.ToManaged.RecordArray(),
        new ParameterExpressions.ToManaged.Class(),
        new ParameterExpressions.ToManaged.Interface(),
    };

    public static string? ToManaged(this GirModel.Parameter from, out string variableName)
    {
        foreach (var converter in Converter)
            if (converter.Supports(from.AnyType))
                return converter.GetExpression(from, out variableName);

        throw new NotImplementedException($"Missing converter to convert from parameter {from.Name} ({from.AnyType}) to managed");
    }
}
