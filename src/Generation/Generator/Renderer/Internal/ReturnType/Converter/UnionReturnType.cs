namespace Generator.Renderer.Internal.ReturnType;

internal class Union : ReturnTypeConverter
{
    public bool Supports(GirModel.ReturnType returnType)
    {
        return returnType.AnyType.Is<GirModel.Union>();
    }

    public RenderableReturnType Convert(GirModel.ReturnType returnType)
    {
        var type = (GirModel.Union) returnType.AnyType.AsT0;

        var nullableTypeName = returnType.IsPointer
            ? Model.Type.Pointer
            : Model.Union.GetFullyQualifiedInternalStructName(type);

        return new RenderableReturnType(nullableTypeName);
    }
}
