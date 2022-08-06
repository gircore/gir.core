using Generator.Model;

namespace Generator.Renderer.Public.ParameterExpressions.ToNative;

internal class StringArray : ParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.IsArray<GirModel.String>();

    public string? GetExpression(GirModel.Parameter parameter, out string variableName)
    {
        var arrayType = parameter.AnyType.AsT1;
        if (parameter.Transfer == GirModel.Transfer.None && arrayType.Length == null)
        {
            variableName = Parameter.GetConvertedName(parameter);
            return $"var {variableName} = new GLib.Internal.StringArrayNullTerminatedSafeHandle({Parameter.GetName(parameter)}).DangerousGetHandle();";
        }

        //We don't need any conversion for string[]
        variableName = Parameter.GetName(parameter);
        return null;
    }
}
