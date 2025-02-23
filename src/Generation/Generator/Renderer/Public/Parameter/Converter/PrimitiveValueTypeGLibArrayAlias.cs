namespace Generator.Renderer.Public.Parameter;

internal class PrimitiveValueTypeGLibArrayAlias : ParameterConverter
{
    public bool Supports(GirModel.AnyType anyType)
    {
        return anyType.IsGLibArrayAlias<GirModel.PrimitiveValueType>();
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
        var alias = (GirModel.Alias) parameter.AnyTypeOrVarArgs.AsT0.AsT1.AnyType.AsT0;
        return $"{Model.Namespace.GetPublicName(alias.Namespace)}.{Model.ArrayType.GetName(parameter.AnyTypeOrVarArgs.AsT0.AsT1)}";
    }

    private static string GetDirection(GirModel.Parameter parameter) => parameter switch
    {
        { Direction: GirModel.Direction.InOut } => ParameterDirection.Ref(),
        { Direction: GirModel.Direction.Out, CallerAllocates: true } => ParameterDirection.Ref(),
        { Direction: GirModel.Direction.Out } => ParameterDirection.Out(),
        _ => ParameterDirection.In()
    };
}
