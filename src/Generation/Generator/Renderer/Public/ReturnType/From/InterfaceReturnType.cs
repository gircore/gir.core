using Generator.Model;

namespace Generator.Renderer.Public;

internal static class InterfaceReturnType
{
    public static RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        var nullableTypeName = ComplexType.GetFullyQualified((GirModel.Interface) returnType.AnyType.AsT0);

        return new RenderableReturnType(nullableTypeName);
    }
}
