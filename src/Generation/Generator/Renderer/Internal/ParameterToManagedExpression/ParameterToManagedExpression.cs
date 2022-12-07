using System;
using System.Collections.Generic;

namespace Generator.Renderer.Internal;

internal static class ParameterToManagedExpression
{
    private static readonly List<ParameterToManagedExpressions.ToManagedParameterConverter> Converter = new()
    {
        new ParameterToManagedExpressions.PrimitiveValueType(),
        new ParameterToManagedExpressions.Enumeration(),
        new ParameterToManagedExpressions.Bitfield(),
        new ParameterToManagedExpressions.Pointer(),
        new ParameterToManagedExpressions.String(),
        new ParameterToManagedExpressions.StringArray(),
        new ParameterToManagedExpressions.Record(),
        new ParameterToManagedExpressions.RecordArray(),
        new ParameterToManagedExpressions.Class(),
        new ParameterToManagedExpressions.Interface(),
    };

    public static string? ToManaged(this GirModel.Parameter from, out string variableName)
    {
        foreach (var converter in Converter)
            if (converter.Supports(from.AnyType))
                return converter.GetExpression(from, out variableName);

        throw new NotImplementedException($"Missing converter to convert from parameter {from.Name} ({from.AnyType}) to managed");
    }
}
