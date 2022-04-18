using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator.Renderer.Public;

internal static class ParameterToNativeExpression
{
    private static readonly List<ParameterExpressions.ToNativeParameterConverter> Converter = new()
    {
        new ParameterExpressions.ToNative.PrimitiveValueType(),
        new ParameterExpressions.ToNative.PrimitiveValueTypeArray(),
        new ParameterExpressions.ToNative.Enumeration(),
        new ParameterExpressions.ToNative.Bitfield(),
        new ParameterExpressions.ToNative.String(),
        new ParameterExpressions.ToNative.StringArray(),
        new ParameterExpressions.ToNative.Interface(),
        new ParameterExpressions.ToNative.InterfaceArray(),
        new ParameterExpressions.ToNative.Class(),
        new ParameterExpressions.ToNative.ClassArray(),
        new ParameterExpressions.ToNative.Record(),
        new ParameterExpressions.ToNative.RecordArray(),
        new ParameterExpressions.ToNative.Callback(),
        new ParameterExpressions.ToNative.Pointer(),
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
