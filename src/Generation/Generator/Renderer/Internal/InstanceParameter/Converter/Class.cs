namespace Generator.Renderer.Internal.InstanceParameter;

internal class Class : InstanceParameterConverter
{
    public bool Supports(GirModel.Type type)
    {
        return type is GirModel.Class;
    }

    public RenderableInstanceParameter Convert(GirModel.InstanceParameter instanceParameter)
    {
        return new RenderableInstanceParameter(
            Name: Model.InstanceParameter.GetName(instanceParameter),
            NullableTypeName: Model.Type.Pointer
        );
    }
}
