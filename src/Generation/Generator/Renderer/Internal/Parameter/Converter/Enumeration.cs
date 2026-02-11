namespace Generator.Renderer.Internal.Parameter;

internal class Enumeration : ParameterConverter
{
    public bool Supports(GirModel.AnyType anyType)
    {
        return anyType.Is<GirModel.Enumeration>();
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

    private static string GetNullableTypeName(GirModel.Parameter parameter)
    {
        return parameter switch
        {
            { Direction: GirModel.Direction.Out, IsPointer: true } => Model.ComplexType.GetFullyQualified((GirModel.Enumeration) parameter.AnyTypeOrVarArgs.AsT0.AsT0),
            { Direction: GirModel.Direction.InOut, IsPointer: true } => Model.ComplexType.GetFullyQualified((GirModel.Enumeration) parameter.AnyTypeOrVarArgs.AsT0.AsT0),
            { IsPointer: true } => Model.Type.Pointer,
            _ => Model.ComplexType.GetFullyQualified((GirModel.Enumeration) parameter.AnyTypeOrVarArgs.AsT0.AsT0)
        };
    }

    private static string GetDirection(GirModel.Parameter parameter) => parameter switch
    {
        { Direction: GirModel.Direction.InOut, IsPointer: true } => ParameterDirection.Ref(),
        { Direction: GirModel.Direction.Out, IsPointer: true, CallerAllocates: true } => ParameterDirection.Ref(),
        { Direction: GirModel.Direction.Out, IsPointer: true } => ParameterDirection.Out(),
        _ => ParameterDirection.In()
    };
}
