namespace Generator.Renderer.Internal.Parameter;

internal class EnumerationArray : ParameterConverter
{
    public bool Supports(GirModel.AnyType anyType)
    {
        return anyType.IsArray<GirModel.Enumeration>();
    }

    public RenderableParameter Convert(GirModel.Parameter parameter)
    {
        return new RenderableParameter(
            Attribute: string.Empty,
            Direction: GetDirection(parameter),
            NullableTypeName: GetNullableTypeName(parameter),
            Name: Model.Parameter.GetName(parameter)
        );
    }

    private static string GetDirection(GirModel.Parameter parameter) => parameter switch
    {
        { Direction: GirModel.Direction.InOut } => ParameterDirection.Ref(),
        { Direction: GirModel.Direction.Out, CallerAllocates: true } => ParameterDirection.Ref(),
        { Direction: GirModel.Direction.Out } => ParameterDirection.Out(),
        _ => ParameterDirection.In()
    };

    private static string GetNullableTypeName(GirModel.Parameter parameter)
    {
        var arrayType = parameter.AnyTypeOrVarArgs.AsT0.AsT1;
        var type = (GirModel.Enumeration) arrayType.AnyType.AsT0;

        return arrayType.Length is null
            ? Model.Type.Pointer
            : Model.ComplexType.GetFullyQualified(type) + "[]";
    }
}
