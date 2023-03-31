using Generator.Model;

namespace Generator.Renderer.Public.ReturnType;

internal class PrimitiveValueTypeAlias : ReturnTypeConverter
{
    public RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        var nullableTypeName = returnType.IsPointer
            ? Type.Pointer
            : Type.GetName(((GirModel.Alias) returnType.AnyType.AsT0).Type);

        return new RenderableReturnType(nullableTypeName);
    }

    public bool Supports(GirModel.ReturnType returnType)
        => returnType.AnyType.IsAlias<GirModel.PrimitiveValueType>();
}
