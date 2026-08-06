namespace Generator.Renderer.Internal.Parameter;

internal class EnumerationArray : ParameterConverter
{
    public bool Supports(GirModel.AnyTypeReference anyTypeReference)
    {
        return anyTypeReference.ReferencesArray<GirModel.Enumeration>();
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
        var arrayTypeReference = parameter.AnyTypeReferenceOrVarArgs.AsT0.AsT1;
        var type = (GirModel.Enumeration) arrayTypeReference.AnyTypeReference.AsT0.Type;

        return arrayTypeReference.Length is null
            ? Model.Type.Pointer
            : Model.ComplexType.GetFullyQualified(type) + "[]";
    }
}
