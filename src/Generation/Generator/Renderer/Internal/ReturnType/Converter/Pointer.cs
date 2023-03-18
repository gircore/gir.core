using Generator.Model;

namespace Generator.Renderer.Internal.ReturnType;

internal class Pointer : ReturnTypeConverter
{
    public bool Supports(GirModel.ReturnType returnType)
    {
        return returnType.AnyType.Is<GirModel.Pointer>();
    }

    public RenderableReturnType Convert(GirModel.ReturnType returnType)
    {
        return new RenderableReturnType(Type.Pointer);
    }
}
