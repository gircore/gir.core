using GirModel;

namespace Generator3.Converter.Parameter.ToNative;

internal class InterfaceArray : ParameterConverter
{
    public bool Supports(AnyType type)
        => type.IsArray<GirModel.Interface>();

    public string GetExpression(GirModel.Parameter parameter, out string variableName)
    {
        variableName = parameter.GetConvertedName();
        return $"var {variableName} = {parameter.GetPublicName()}.Select(iface => (iface as GObject.Object).Handle).ToArray();";
    }
}
