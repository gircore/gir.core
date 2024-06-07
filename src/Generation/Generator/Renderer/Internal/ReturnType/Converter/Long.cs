namespace Generator.Renderer.Internal.ReturnType;

internal class Long : ReturnTypeConverter
{
    public bool Supports(GirModel.ReturnType returnType)
    {
        return returnType.AnyType.Is<GirModel.Long>();
    }

    public RenderableReturnType Convert(GirModel.ReturnType returnType)
    {
        var nullableTypeName = returnType.IsPointer
            ? Model.Type.Pointer
            : "CLong";

        return new RenderableReturnType(nullableTypeName);
    }
}
