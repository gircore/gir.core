namespace Generator.Renderer.Public.Parameter;

internal class Void : ParameterConverter
{
    public bool Supports(GirModel.AnyTypeReference anyTypeReference)
    {
        return anyTypeReference.References<GirModel.Void>();
    }

    public ParameterTypeData Create(GirModel.Parameter parameter)
    {
        return new ParameterTypeData(
            Direction: string.Empty,
            NullableTypeName: Model.Type.Pointer
        );
    }
}
