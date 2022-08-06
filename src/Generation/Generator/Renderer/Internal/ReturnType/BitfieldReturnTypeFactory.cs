using Generator.Model;

namespace Generator.Renderer.Internal;

internal static class BitfieldReturnTypeFactory
{
    public static RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        var type = (GirModel.Bitfield) returnType.AnyType.AsT0;
        return new RenderableReturnType(ComplexType.GetFullyQualified(type));
    }
}
