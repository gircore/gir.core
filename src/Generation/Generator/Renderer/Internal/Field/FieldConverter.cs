namespace Generator.Renderer.Internal.Field;

public interface FieldConverter
{
    bool Supports(GirModel.Field field);
    RenderableField Convert(GirModel.Field field);
}
