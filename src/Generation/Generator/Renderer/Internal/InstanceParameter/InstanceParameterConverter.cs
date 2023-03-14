namespace Generator.Renderer.Internal.InstanceParameter;

public interface InstanceParameterConverter
{
    bool Supports(GirModel.Type type);
    RenderableInstanceParameter Convert(GirModel.InstanceParameter instanceParameter);
}
