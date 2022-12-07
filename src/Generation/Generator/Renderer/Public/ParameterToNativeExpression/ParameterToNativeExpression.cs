using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator.Renderer.Public;

internal static class ParameterToNativeExpression
{
    private static readonly List<ParameterToNativeExpressions.ToNativeParameterConverter> Converter = new()
    {
        new ParameterToNativeExpressions.PrimitiveValueType(),
        new ParameterToNativeExpressions.PrimitiveValueTypeArray(),
        new ParameterToNativeExpressions.Enumeration(),
        new ParameterToNativeExpressions.Bitfield(),
        new ParameterToNativeExpressions.String(),
        new ParameterToNativeExpressions.StringArray(),
        new ParameterToNativeExpressions.Interface(),
        new ParameterToNativeExpressions.InterfaceArray(),
        new ParameterToNativeExpressions.Class(),
        new ParameterToNativeExpressions.ClassArray(),
        new ParameterToNativeExpressions.Record(),
        new ParameterToNativeExpressions.RecordArray(),
        new ParameterToNativeExpressions.Callback(),
        new ParameterToNativeExpressions.Pointer(),
    };

    public static IReadOnlyList<ParameterToNativeData> Initialize(IEnumerable<GirModel.Parameter> parameters)
    {
        var parameterToNativeDatas = new List<ParameterToNativeData>(parameters.Select(x => new ParameterToNativeData(x)));

        foreach (var parameter in parameterToNativeDatas)
        {
            var converterFound = false;

            foreach (var converter in Converter)
            {
                if (converter.Supports(parameter.Parameter.AnyType))
                {
                    converter.Initialize(parameter, parameterToNativeDatas);
                    converterFound = true;
                    break;
                }
            }

            if (!converterFound)
                throw new NotImplementedException($"Missing converter to convert from parameter {parameter.Parameter} ({parameter.Parameter.AnyType}) to native");
        }

        return parameterToNativeDatas;
    }
}
