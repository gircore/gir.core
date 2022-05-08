using Generator.Model;

namespace Generator.Renderer.Internal;

internal static class ArrayPrimitiveValueReturnTypeFactory
{
    public static RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        return new RenderableReturnType(ArrayType.GetName(returnType.AnyType.AsT1));
    }
}
