namespace Generator.Renderer.Internal.Parameter;

internal class UnionArray : ParameterConverter
{
    public bool Supports(GirModel.AnyTypeReference anyTypeReference)
    {
        return anyTypeReference.ReferencesArray<GirModel.Union>();
    }

    public RenderableParameter Convert(GirModel.Parameter parameter)
    {
        if (parameter.AnyTypeReferenceOrVarArgs.AsT0.AsT1.IsPointer)
            return PointerArray(parameter);

        return DataArray(parameter);
    }

    private static RenderableParameter PointerArray(GirModel.Parameter parameter)
    {
        return new RenderableParameter(
            Attribute: string.Empty,
            Direction: string.Empty,
            NullableTypeName: $"ref {Model.Type.Pointer}",
            Name: Model.Parameter.GetName(parameter)
        );
    }

    private static RenderableParameter DataArray(GirModel.Parameter parameter)
    {
        var union = (GirModel.Union) parameter.AnyTypeReferenceOrVarArgs.AsT0.AsT1.AnyTypeReference.AsT0.Type;

        return new RenderableParameter(
            Attribute: string.Empty,
            Direction: string.Empty,
            NullableTypeName: Model.Union.GetFullyQualifiedInternalStructName(union) + "[]",
            Name: Model.Parameter.GetName(parameter)
        );
    }
}
