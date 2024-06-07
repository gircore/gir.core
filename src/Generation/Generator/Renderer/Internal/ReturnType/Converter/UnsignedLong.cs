namespace Generator.Renderer.Internal.ReturnType;

internal class UnsignedLong : ReturnTypeConverter
{
    public bool Supports(GirModel.ReturnType returnType)
    {
        return returnType.AnyType.Is<GirModel.UnsignedLong>();
    }

    public RenderableReturnType Convert(GirModel.ReturnType returnType)
    {
        var nullableTypeName = returnType.IsPointer
            ? Model.Type.Pointer
            : "CULong";

        return new RenderableReturnType(nullableTypeName);
    }
}
