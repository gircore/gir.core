namespace Generator.Renderer.Public.Constant;

internal interface ConstantsConverter
{
    bool Supports(GirModel.Type type);
    RenderableConstant Convert(GirModel.Constant constant);
}
