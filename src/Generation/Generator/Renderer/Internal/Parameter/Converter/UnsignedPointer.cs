namespace Generator.Renderer.Internal.Parameter;

internal class UnsignedPointer : ParameterConverter
{
    public bool Supports(GirModel.AnyType anyType)
    {
        return anyType.Is<GirModel.UnsignedPointer>();
    }

    public RenderableParameter Convert(GirModel.Parameter parameter)
    {
        //IntPtr can't be nullable. They can be "nulled" via IntPtr.Zero.
        return new RenderableParameter(
            Attribute: string.Empty,
            Direction: string.Empty,
            NullableTypeName: Model.Type.GetName(parameter.AnyTypeOrVarArgs.AsT0.AsT0),
            Name: Model.Parameter.GetName(parameter)
        );
    }
}
