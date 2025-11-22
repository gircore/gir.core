using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator.Renderer.Public;

internal static class ParameterToNativeExpression
{
    private static readonly List<ParameterToNativeExpressions.ToNativeParameterConverter> Converter = new()
    {
        new ParameterToNativeExpressions.Bitfield(),
        new ParameterToNativeExpressions.Callback(),
        new ParameterToNativeExpressions.Class(),
        new ParameterToNativeExpressions.ClassArray(),
        new ParameterToNativeExpressions.Enumeration(),
        new ParameterToNativeExpressions.ForeignTypedRecord(),
        new ParameterToNativeExpressions.ForeignTypedRecordArray(),
        new ParameterToNativeExpressions.GLibPointerArray(),
        new ParameterToNativeExpressions.Interface(),
        new ParameterToNativeExpressions.InterfaceArray(),
        new ParameterToNativeExpressions.OpaqueTypedRecord(),
        new ParameterToNativeExpressions.OpaqueTypedRecordArray(),
        new ParameterToNativeExpressions.OpaqueUntypedRecord(),
        new ParameterToNativeExpressions.OpaqueUntypedRecordArray(),
        new ParameterToNativeExpressions.PlatformString(),
        new ParameterToNativeExpressions.PlatformStringArray(),
        new ParameterToNativeExpressions.Pointer(),
        new ParameterToNativeExpressions.PointerAlias(),
        new ParameterToNativeExpressions.Long(), //Must be before primitive value type
        new ParameterToNativeExpressions.CLong(), //Must be before primitive value type
        new ParameterToNativeExpressions.UnsignedLong(), //Must be before primitive value type
        new ParameterToNativeExpressions.UnsignedCLong(), //Must be before primitive value type
        new ParameterToNativeExpressions.PrimitiveValueType(),
        new ParameterToNativeExpressions.PrimitiveValueTypeAlias(),
        new ParameterToNativeExpressions.PrimitiveValueTypeArray(),
        new ParameterToNativeExpressions.TypedRecord(),
        new ParameterToNativeExpressions.TypedRecordArray(),
        new ParameterToNativeExpressions.UntypedRecord(),
        new ParameterToNativeExpressions.UntypedRecordArray(),
        new ParameterToNativeExpressions.Utf8String(),
        new ParameterToNativeExpressions.Utf8StringArray(),
    };

    public static IReadOnlyList<ParameterToNativeData> Initialize(IEnumerable<GirModel.Parameter> parameters)
    {
        var parameterToNativeDatas = new List<ParameterToNativeData>(parameters.Select(x => new ParameterToNativeData(x)));

        foreach (var parameter in parameterToNativeDatas)
        {
            var converterFound = false;

            foreach (var converter in Converter)
            {
                if (parameter.Parameter.AnyTypeOrVarArgs.IsT1)
                    throw new Exception("Variadic parameters are not yet supported");

                if (converter.Supports(parameter.Parameter.AnyTypeOrVarArgs.AsT0))
                {
                    converter.Initialize(parameter, parameterToNativeDatas);
                    converterFound = true;
                    break;
                }
            }

            if (!converterFound)
                throw new NotImplementedException($"Missing converter to convert from parameter {parameter.Parameter} ({parameter.Parameter.AnyTypeOrVarArgs}) to native");
        }

        return parameterToNativeDatas;
    }
}
