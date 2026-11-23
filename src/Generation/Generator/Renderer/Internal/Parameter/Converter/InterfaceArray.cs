namespace Generator.Renderer.Internal.Parameter;

internal class InterfaceArray : ParameterConverter
{
    public bool Supports(GirModel.AnyTypeReference anyTypeReference)
    {
        return anyTypeReference.ReferencesArray<GirModel.Interface>();
    }

    public RenderableParameter Convert(GirModel.Parameter parameter)
    {
        return new RenderableParameter(
            Attribute: GetAttribute(parameter),
            Direction: string.Empty,
            NullableTypeName: GetNullableTypeName(parameter),
            Name: Model.Parameter.GetName(parameter)
        );
    }

    private static string GetAttribute(GirModel.Parameter parameter)
    {
        return parameter.AnyTypeReferenceOrVarArgs.AsT0.AsArrayTypeReference.Length switch
        {
            { } length => MarshalAs.UnmanagedLpArray(sizeParamIndex: length),
            _ => string.Empty,
        };
    }

    private static string GetNullableTypeName(GirModel.Parameter parameter)
    {
        return parameter.AnyTypeReferenceOrVarArgs.AsT0.AsArrayTypeReference.Length is null
            ? Model.Type.Pointer
            : Model.Type.PointerArray;
    }
}
