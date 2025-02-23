namespace Generator.Renderer.Internal.ReturnType;

public interface ReturnTypeConverter
{
    bool Supports(GirModel.ReturnType returnType);
    RenderableReturnType Convert(GirModel.ReturnType returnType);
}
