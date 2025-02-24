namespace Generator.Renderer.Internal.ReturnType;

internal class PointerAlias : ReturnTypeConverter
{
    public bool Supports(GirModel.ReturnType returnType)
    {
        return returnType.AnyType.IsAlias<GirModel.Pointer>();
    }

    public RenderableReturnType Convert(GirModel.ReturnType returnType)
    {
        return new RenderableReturnType(Model.Type.Pointer);
    }
}
