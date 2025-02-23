namespace Generator.Renderer.Public.ReturnType;

internal interface ReturnTypeConverter
{
    RenderableReturnType Create(GirModel.ReturnType returnType);
    bool Supports(GirModel.ReturnType returnType);
}
