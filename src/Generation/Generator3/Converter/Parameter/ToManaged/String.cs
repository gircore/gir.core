namespace Generator3.Converter.Parameter.ToManaged;

internal class String : ParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.String>();

    public string? GetExpression(GirModel.Parameter parameter, out string variableName)
    {
        variableName = parameter.GetPublicName();
        return null;
    }
}
