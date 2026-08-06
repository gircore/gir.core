namespace Generator.Renderer.Internal.Parameter;

internal class UnsignedPointer : ParameterConverter
{
    public bool Supports(GirModel.AnyTypeReference anyTypeReference)
    {
        return anyTypeReference.References<GirModel.UnsignedPointer>();
    }

    public RenderableParameter Convert(GirModel.Parameter parameter)
    {
        //IntPtr can't be nullable. They can be "nulled" via IntPtr.Zero.
        return new RenderableParameter(
            Attribute: string.Empty,
            Direction: string.Empty,
            NullableTypeName: Model.Type.GetName(parameter.AnyTypeReferenceOrVarArgs.AsT0.AsT0.Type),
            Name: Model.Parameter.GetName(parameter)
        );
    }
}
