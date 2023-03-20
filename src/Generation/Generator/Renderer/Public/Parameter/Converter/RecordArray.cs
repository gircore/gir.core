namespace Generator.Renderer.Public.Parameter;

internal class RecordArray : ParameterConverter
{
    public bool Supports(GirModel.AnyType anyType)
    {
        return anyType.IsArray<GirModel.Record>();
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
        var arrayType = parameter.AnyTypeOrVarArgs.AsT0.AsT1;
        return Model.ComplexType.GetFullyQualified((GirModel.Record) arrayType.AnyType.AsT0) + "[]";
    }

    private static string GetDirection(GirModel.Parameter parameter) => parameter switch
    {
        { Direction: GirModel.Direction.InOut } => ParameterDirection.Ref(),
        { Direction: GirModel.Direction.Out, CallerAllocates: true } => ParameterDirection.Ref(),
        { Direction: GirModel.Direction.Out } => ParameterDirection.Out(),
        _ => ParameterDirection.In()
    };
}
