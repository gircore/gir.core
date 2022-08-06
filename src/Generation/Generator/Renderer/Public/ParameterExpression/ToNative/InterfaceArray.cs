using Generator.Model;

namespace Generator.Renderer.Public.ParameterExpressions.ToNative;

internal class InterfaceArray : ParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.IsArray<GirModel.Interface>();

    public string GetExpression(GirModel.Parameter parameter, out string variableName)
    {
        variableName = Parameter.GetConvertedName(parameter);
        return $"var {variableName} = {Parameter.GetName(parameter)}.Select(iface => (iface as GObject.Object).Handle).ToArray();";
    }
}
