namespace Generator.Renderer.Internal.ReturnType;

internal class GLibPointerArray : ReturnTypeConverter
{
    public bool Supports(GirModel.ReturnType returnType)
    {
        return returnType.AnyType.IsGLibPtrArray();
    }

    public RenderableReturnType Convert(GirModel.ReturnType returnType)
    {
        var typeName = returnType switch
        {
            { Transfer: GirModel.Transfer.Full } => "GLib.Internal.PtrArrayOwnedHandle",
            _ => "GLib.Internal.PtrArrayUnownedHandle"
        };

        return new RenderableReturnType(typeName);
    }
}
