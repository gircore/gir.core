namespace Generator.Renderer.Public.ReturnType;

internal class String : ReturnTypeConverter
{
    public RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        return new RenderableReturnType("string" + Nullable.Render(returnType));
    }

    public bool Supports(GirModel.ReturnType returnType)
        => returnType.AnyType.Is<GirModel.String>();
}
