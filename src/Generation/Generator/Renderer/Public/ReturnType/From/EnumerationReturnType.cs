using Generator.Model;

namespace Generator.Renderer.Public;

internal static class EnumerationReturnType
{
    public static RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        var nullableTypeName = ComplexType.GetFullyQualified((GirModel.Enumeration) returnType.AnyType.AsT0);

        return new RenderableReturnType(nullableTypeName);
    }
}
