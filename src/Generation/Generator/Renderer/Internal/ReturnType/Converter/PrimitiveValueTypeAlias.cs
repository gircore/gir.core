namespace Generator.Renderer.Internal.ReturnType;

internal class PrimitiveValueTypeAlias : ReturnTypeConverter
{
    public bool Supports(GirModel.ReturnType returnType)
    {
        return returnType.AnyTypeReference.ReferencesAlias<GirModel.PrimitiveValueType>();
    }

    public RenderableReturnType Convert(GirModel.ReturnType returnType)
    {
        var nullableTypeName = returnType.IsPointer
            ? Model.Type.Pointer
            : Model.Type.GetName(((GirModel.Alias) returnType.AnyTypeReference.AsTypeReference.Type).Type);

        return new RenderableReturnType(nullableTypeName);
    }
}
