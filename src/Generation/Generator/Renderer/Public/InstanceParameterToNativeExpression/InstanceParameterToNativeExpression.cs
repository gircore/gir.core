using System.Collections.Generic;

namespace Generator.Renderer.Public;

internal static class InstanceParameterToNativeExpression
{
    private static readonly List<InstanceParameterToNativeExpressions.InstanceParameterConverter> Converter = new()
    {
        new InstanceParameterToNativeExpressions.Class(),
        new InstanceParameterToNativeExpressions.Interface(),
        new InstanceParameterToNativeExpressions.OpaqueTypedRecord(),
        new InstanceParameterToNativeExpressions.OpaqueUntypedRecord(),
        new InstanceParameterToNativeExpressions.Pointer(),
        new InstanceParameterToNativeExpressions.TypedRecord()
    };

    public static string Render(GirModel.InstanceParameter instanceParameter)
    {
        foreach (var converter in Converter)
            if (converter.Supports(instanceParameter.Type))
                return converter.GetExpression(instanceParameter);

        throw new System.NotImplementedException($"Missing converter to convert from instance parameter to native expression: {instanceParameter}.");
    }
}
