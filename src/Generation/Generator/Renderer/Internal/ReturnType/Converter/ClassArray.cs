namespace Generator.Renderer.Internal.ReturnType;

internal class ClassArray : ReturnTypeConverter
{
    public bool Supports(GirModel.ReturnType returnType)
    {
        return returnType.AnyTypeReference.ReferencesArray<GirModel.Class>();
    }

    public RenderableReturnType Convert(GirModel.ReturnType returnType)
    {
        return new RenderableReturnType(Model.Type.PointerArray);
    }
}
