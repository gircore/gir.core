namespace Generator.Renderer.Internal.ReturnType;

internal class Union : ReturnTypeConverter
{
    public bool Supports(GirModel.ReturnType returnType)
    {
        return returnType.AnyTypeReference.References<GirModel.Union>();
    }

    public RenderableReturnType Convert(GirModel.ReturnType returnType)
    {
        var type = (GirModel.Union) returnType.AnyTypeReference.AsT0.Type;

        var nullableTypeName = returnType.IsPointer
            ? Model.Type.Pointer
            : Model.Union.GetFullyQualifiedInternalStructName(type);

        return new RenderableReturnType(nullableTypeName);
    }
}
