namespace Generator.Renderer.Internal.ReturnType;

internal class CLong : ReturnTypeConverter
{
    public bool Supports(GirModel.ReturnType returnType)
    {
        return returnType.AnyType.Is<GirModel.CLong>();
    }

    public RenderableReturnType Convert(GirModel.ReturnType returnType)
    {
        var nullableTypeName = returnType.IsPointer
            ? Model.Type.Pointer
            : "CLong";

        return new RenderableReturnType(nullableTypeName);
    }
}
