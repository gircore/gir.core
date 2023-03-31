using Generator.Model;

namespace Generator.Renderer.Public.ReturnType;

internal class PrimitiveValueTypeAlias : ReturnTypeConverter
{
    public RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        var alias = (GirModel.Alias) returnType.AnyType.AsT0;

        var nullableTypeName = returnType.IsPointer
            ? Type.Pointer
            : $"{Namespace.GetPublicName(alias.Namespace)}.{Type.GetName(alias)}";

        return new RenderableReturnType(nullableTypeName);
    }

    public bool Supports(GirModel.ReturnType returnType)
        => returnType.AnyType.IsAlias<GirModel.PrimitiveValueType>();
}
