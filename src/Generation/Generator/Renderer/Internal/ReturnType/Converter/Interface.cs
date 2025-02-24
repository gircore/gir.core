using Generator.Model;

namespace Generator.Renderer.Internal.ReturnType;

internal class Interface : ReturnTypeConverter
{
    public bool Supports(GirModel.ReturnType returnType)
    {
        return returnType.AnyType.Is<GirModel.Interface>();
    }

    public RenderableReturnType Convert(GirModel.ReturnType returnType)
    {
        var nullableTypeName = returnType.IsPointer
            ? Type.Pointer
            : Type.GetName(returnType.AnyType.AsT0);

        return new RenderableReturnType(nullableTypeName);
    }
}
