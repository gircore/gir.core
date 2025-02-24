namespace Generator.Renderer.Internal.Parameter;

internal class PointerAlias : ParameterConverter
{
    public bool Supports(GirModel.AnyType anyType)
    {
        return anyType.IsAlias<GirModel.Pointer>();
    }

    public RenderableParameter Convert(GirModel.Parameter parameter)
    {
        return new RenderableParameter(
            Attribute: string.Empty,
            Direction: string.Empty,
            NullableTypeName: Model.Type.Pointer,
            Name: Model.Parameter.GetName(parameter)
        );
    }
}
