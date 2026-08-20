namespace Generator.Renderer.Internal.ReturnType;

internal class Utf8StringInCallback : ReturnTypeConverter
{
    public bool Supports(GirModel.ReturnType returnType)
    {
        return returnType.AnyTypeReference.References<GirModel.Utf8String>();
    }

    public RenderableReturnType Convert(GirModel.ReturnType returnType)
    {
        // This must be IntPtr since SafeHandle's cannot be returned from managed to unmanaged.
        return new RenderableReturnType(Model.Type.Pointer);
    }
}
