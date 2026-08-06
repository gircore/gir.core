namespace Generator.Renderer.Public.ReturnType;

internal class Void : ReturnTypeConverter
{
    public RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        return new RenderableReturnType("void");
    }

    public bool Supports(GirModel.ReturnType returnType)
        => returnType.AnyTypeReference.References<GirModel.Void>();
}
