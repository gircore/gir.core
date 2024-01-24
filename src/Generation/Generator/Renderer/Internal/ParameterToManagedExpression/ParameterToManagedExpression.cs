using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator.Renderer.Internal;

internal static class ParameterToManagedExpression
{
    private static readonly List<ParameterToManagedExpressions.ToManagedParameterConverter> Converter = new()
    {
        new ParameterToManagedExpressions.Bitfield(),
        new ParameterToManagedExpressions.Callback(),
        new ParameterToManagedExpressions.Class(),
        new ParameterToManagedExpressions.Enumeration(),
        new ParameterToManagedExpressions.ForeignTypedRecord(),
        new ParameterToManagedExpressions.Interface(),
        new ParameterToManagedExpressions.OpaqueTypedRecord(),
        new ParameterToManagedExpressions.OpaqueUntypedRecord(),
        new ParameterToManagedExpressions.PlatformStringArray(),
        new ParameterToManagedExpressions.Pointer(),
        new ParameterToManagedExpressions.PointerAlias(),
        new ParameterToManagedExpressions.PrimitiveValueType(),
        new ParameterToManagedExpressions.PrimitiveValueTypeAlias(),
        new ParameterToManagedExpressions.PrimitiveValueTypeArray(),
        new ParameterToManagedExpressions.PrimitiveValueTypeArrayAlias(),
        new ParameterToManagedExpressions.Record(),
        new ParameterToManagedExpressions.RecordArray(),
        new ParameterToManagedExpressions.String(),
        new ParameterToManagedExpressions.TypedRecord(),
        new ParameterToManagedExpressions.TypedRecordArray(),
        new ParameterToManagedExpressions.Utf8StringArray(),
    };

    public static IReadOnlyList<ParameterToManagedData> Initialize(IEnumerable<GirModel.Parameter> parameters)
    {
        var parameterToManagedDatas = new List<ParameterToManagedData>(parameters.Select(x => new ParameterToManagedData(x)));

        foreach (var parameter in parameterToManagedDatas)
        {
            var converterFound = false;

            foreach (var converter in Converter)
            {
                if (parameter.Parameter.AnyTypeOrVarArgs.IsT1)
                    throw new Exception("Can't convert to managed: Variadic parameters are not yet supported");

                if (converter.Supports(parameter.Parameter.AnyTypeOrVarArgs.AsT0))
                {
                    converter.Initialize(parameter, parameterToManagedDatas);
                    converterFound = true;
                    break;
                }
            }

            if (!converterFound)
                throw new NotImplementedException($"Missing converter to convert from parameter {parameter.Parameter} ({parameter.Parameter.AnyTypeOrVarArgs}) to managed");
        }

        return parameterToManagedDatas;
    }
}
