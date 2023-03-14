using Generator.Model;

namespace Generator.Renderer.Public.ReturnType;

internal class PrimitiveValueType : ReturnTypeConverter
{
    public RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        var nullableTypeName = returnType.IsPointer
            ? Type.Pointer
            : Type.GetName(returnType.AnyType.AsT0);

        return new RenderableReturnType(nullableTypeName);
    }

    public bool Supports(GirModel.ReturnType returnType)
        => returnType.AnyType.Is<GirModel.PrimitiveValueType>();
}
