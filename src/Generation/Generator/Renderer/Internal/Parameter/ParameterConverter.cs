namespace Generator.Renderer.Internal.Parameter;

public interface ParameterConverter
{
    bool Supports(GirModel.AnyTypeReference anyTypeReference);
    RenderableParameter Convert(GirModel.Parameter parameter);
}
