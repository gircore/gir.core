using Generator.Model;

namespace Generator.Renderer.Internal;

internal static class EnumerationReturnTypeFactory
{
    public static RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        var type = (GirModel.Enumeration) returnType.AnyType.AsT0;
        return new RenderableReturnType(ComplexType.GetFullyQualified(type));
    }
}
