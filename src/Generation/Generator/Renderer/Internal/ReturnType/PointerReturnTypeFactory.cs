using Generator.Model;

namespace Generator.Renderer.Internal;

internal static class PointerReturnTypeFactory
{
    public static RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        return new RenderableReturnType(Type.Pointer);
    }
}
