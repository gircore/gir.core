namespace Generator.Renderer.Internal.Parameter;

internal class ClassArray : ParameterConverter
{
    public bool Supports(GirModel.AnyTypeReference anyTypeReference)
    {
        return anyTypeReference.ReferencesArray<GirModel.Class>();
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
        return parameter.AnyTypeReferenceOrVarArgs.AsT0.AsT1.Length switch
        {
            { } length => MarshalAs.UnmanagedLpArray(sizeParamIndex: length),
            _ => string.Empty,
        };
    }

    private static string GetNullableTypeName(GirModel.Parameter parameter)
    {
        return parameter.AnyTypeReferenceOrVarArgs.AsT0.AsT1.Length is null
            ? Model.Type.Pointer
            : Model.Type.PointerArray;
    }
}
