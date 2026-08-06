namespace Generator.Renderer.Internal.ReturnType;

internal class Long : ReturnTypeConverter
{
    public bool Supports(GirModel.ReturnType returnType)
    {
        return returnType.AnyTypeReference.References<GirModel.Long>();
    }

    public RenderableReturnType Convert(GirModel.ReturnType returnType)
    {
        var nullableTypeName = returnType.IsPointer
            ? Model.Type.Pointer
            : Model.Type.GetName(returnType.AnyTypeReference.AsT0.Type);

        return new RenderableReturnType(nullableTypeName);
    }
}
