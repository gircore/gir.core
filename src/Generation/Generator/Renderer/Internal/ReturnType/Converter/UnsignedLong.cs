namespace Generator.Renderer.Internal.ReturnType;

internal class UnsignedLong : ReturnTypeConverter
{
    public bool Supports(GirModel.ReturnType returnType)
    {
        return returnType.AnyTypeReference.References<GirModel.UnsignedLong>();
    }

    public RenderableReturnType Convert(GirModel.ReturnType returnType)
    {
        var nullableTypeName = returnType.IsPointer
            ? Model.Type.Pointer
            : Model.Type.GetName(returnType.AnyTypeReference.AsT0.Type);

        return new RenderableReturnType(nullableTypeName);
    }
}
