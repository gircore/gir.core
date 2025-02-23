namespace Generator.Renderer.Public.Field;

public interface FieldConverter
{
    bool Supports(GirModel.Field field);
    RenderableField Convert(GirModel.Field field);
}
