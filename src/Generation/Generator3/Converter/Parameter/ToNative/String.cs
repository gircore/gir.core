using GirModel;

namespace Generator3.Converter.Parameter.ToNative;

internal class String : ParameterConverter
{
    public bool Supports(AnyType type)
        => type.Is<GirModel.String>();

    public string? GetExpression(GirModel.Parameter parameter, out string variableName)
    {
        variableName = parameter.GetPublicName();
        return null;
    }
}
