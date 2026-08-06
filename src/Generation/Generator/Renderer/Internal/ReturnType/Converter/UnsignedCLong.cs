namespace Generator.Renderer.Internal.ReturnType;

internal class UnsignedCLong : ReturnTypeConverter
{
    public bool Supports(GirModel.ReturnType returnType)
    {
        return returnType.AnyTypeReference.References<GirModel.UnsignedCLong>();
    }

    public RenderableReturnType Convert(GirModel.ReturnType returnType)
    {
        var nullableTypeName = returnType.IsPointer
            ? Model.Type.Pointer
            : "CULong";

        return new RenderableReturnType(nullableTypeName);
    }
}
