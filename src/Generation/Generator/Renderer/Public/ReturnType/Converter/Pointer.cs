using Generator.Model;

namespace Generator.Renderer.Public.ReturnType;

internal class Pointer : ReturnTypeConverter
{
    public RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        return new RenderableReturnType(Type.Pointer);
    }

    public bool Supports(GirModel.ReturnType returnType)
        => returnType.AnyType.Is<GirModel.Pointer>();
}
