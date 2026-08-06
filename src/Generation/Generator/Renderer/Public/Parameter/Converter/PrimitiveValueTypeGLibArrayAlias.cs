namespace Generator.Renderer.Public.Parameter;

internal class PrimitiveValueTypeGLibArrayAlias : ParameterConverter
{
    public bool Supports(GirModel.AnyTypeReference anyTypeReference)
    {
        return anyTypeReference.ReferencesGLibArrayAlias<GirModel.PrimitiveValueType>();
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
        var alias = (GirModel.Alias) parameter.AnyTypeReferenceOrVarArgs.AsT0.AsT1.AnyTypeReference.AsT0.Type;
        return $"{Model.Namespace.GetPublicName(alias.Namespace)}.{Model.ArrayType.GetName(parameter.AnyTypeReferenceOrVarArgs.AsT0.AsT1)}";
    }

    private static string GetDirection(GirModel.Parameter parameter) => parameter switch
    {
        { Direction: GirModel.Direction.InOut } => ParameterDirection.Ref(),
        { Direction: GirModel.Direction.Out, CallerAllocates: true } => ParameterDirection.Ref(),
        { Direction: GirModel.Direction.Out } => ParameterDirection.Out(),
        _ => ParameterDirection.In()
    };
}
