namespace Generator.Renderer.Public.Parameter;

internal class Bitfield : ParameterConverter
{
    public bool Supports(GirModel.AnyType anyType)
    {
        return anyType.Is<GirModel.Bitfield>();
    }

    public ParameterTypeData Create(GirModel.Parameter parameter)
    {
        return new ParameterTypeData(
            Direction: GetDirection(parameter),
            NullableTypeName: GetNullableTypeName(parameter)
        );
    }

    private static string GetNullableTypeName(GirModel.Parameter parameter)
    {
        var type = (GirModel.Bitfield) parameter.AnyTypeOrVarArgs.AsT0.AsT0;
        return Model.ComplexType.GetFullyQualified(type);
    }

    private static string GetDirection(GirModel.Parameter parameter) => parameter switch
    {
        { Direction: GirModel.Direction.InOut } => ParameterDirection.Ref(),
        { Direction: GirModel.Direction.Out, CallerAllocates: true } => ParameterDirection.Ref(),
        { Direction: GirModel.Direction.Out } => ParameterDirection.Out(),
        { IsPointer: true } => ParameterDirection.Ref(),
        _ => ParameterDirection.In()
    };
}
