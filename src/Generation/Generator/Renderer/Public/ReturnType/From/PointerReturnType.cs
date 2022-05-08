using Generator.Model;

namespace Generator.Renderer.Public;

internal static class PointerReturnType
{
    public static RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        return new RenderableReturnType(Type.Pointer);
    }
}
