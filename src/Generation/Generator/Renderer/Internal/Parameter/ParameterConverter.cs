namespace Generator.Renderer.Internal.Parameter;

public interface ParameterConverter
{
    bool Supports(GirModel.AnyType anyType);
    RenderableParameter Convert(GirModel.Parameter parameter);
}
