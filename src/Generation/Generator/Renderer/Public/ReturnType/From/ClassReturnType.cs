using Generator.Model;

namespace Generator.Renderer.Public;

internal static class ClassReturnType
{
    public static RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        var nullableTypeName = ComplexType.GetFullyQualified((GirModel.Class) returnType.AnyType.AsT0);

        return new RenderableReturnType(nullableTypeName);
    }
}
