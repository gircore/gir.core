namespace Generator.Renderer.Public.ReturnType;

internal class GLibPointerArray : ReturnTypeConverter
{
    public bool Supports(GirModel.ReturnType returnType)
    {
        return returnType.AnyTypeReference.ReferencesGLibPtrArray();
    }

    public RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        return new RenderableReturnType(Model.PointerArrayType.GetFullyQualifiedPublicClassName() + Nullable.Render(returnType));
    }
}
