namespace Generator.Renderer.Public.Parameter;

internal class PrimitiveValueTypeAlias : ParameterConverter
{
    public bool Supports(GirModel.AnyTypeReference anyTypeReference)
    {
        return anyTypeReference.ReferencesAlias<GirModel.PrimitiveValueType>();
    }

    public ParameterTypeData Create(GirModel.Parameter parameter)
    {
        var alias = (GirModel.Alias) parameter.AnyTypeReferenceOrVarArgs.AsT0.AsT0.Type;

        return new ParameterTypeData(
            Direction: GetDirection(parameter),
            NullableTypeName: Model.Namespace.GetPublicName(alias.Namespace) + "." + Model.Type.GetName(alias)
        );
    }

    private static string GetDirection(GirModel.Parameter parameter) => parameter switch
    {
        { Direction: GirModel.Direction.InOut } => ParameterDirection.Ref(),
        { Direction: GirModel.Direction.Out } => ParameterDirection.Out(),
        { Direction: GirModel.Direction.In, IsPointer: true } => ParameterDirection.Ref(),
        _ => ParameterDirection.In()
    };
}
