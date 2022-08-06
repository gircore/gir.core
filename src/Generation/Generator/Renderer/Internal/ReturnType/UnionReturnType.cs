using Generator.Model;

namespace Generator.Renderer.Internal;

internal static class UnionReturnTypeFactory
{
    public static RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        var type = (GirModel.Union) returnType.AnyType.AsT0;

        var nullableTypeName = returnType.IsPointer
            ? Type.Pointer
            : Union.GetFullyQualifiedInternalStructName(type);

        return new RenderableReturnType(nullableTypeName);
    }
}
