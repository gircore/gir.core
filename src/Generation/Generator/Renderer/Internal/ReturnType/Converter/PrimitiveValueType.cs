namespace Generator.Renderer.Internal.ReturnType;

internal class PrimitiveValueType : ReturnTypeConverter
{
    public bool Supports(GirModel.ReturnType returnType)
    {
        return returnType.AnyType.Is<GirModel.PrimitiveValueType>();
    }

    public RenderableReturnType Convert(GirModel.ReturnType returnType)
    {
        var nullableTypeName = returnType.IsPointer
            ? Model.Type.Pointer
            : Model.Type.GetName(returnType.AnyType.AsT0);

        return new RenderableReturnType(nullableTypeName);
    }
}
