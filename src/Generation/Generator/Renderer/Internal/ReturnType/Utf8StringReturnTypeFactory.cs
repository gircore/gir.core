using Generator.Model;

namespace Generator.Renderer.Internal;

internal static class Utf8StringReturnTypeFactory
{
    public static RenderableReturnType CreateForCallback(GirModel.ReturnType returnType)
    {
        // This must be IntPtr since SafeHandle's cannot be returned from managed to unmanaged.
        return new RenderableReturnType(Type.Pointer);
    }

    public static RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        var nullableTypeName = returnType switch
        {
            { Nullable: true, Transfer: GirModel.Transfer.None } => Utf8String.GetInternalNullableUnownedHandleName(),
            { Nullable: false, Transfer: GirModel.Transfer.None } => Utf8String.GetInternalNonNullableUnownedHandleName(),
            { Nullable: true, Transfer: GirModel.Transfer.Full } => Utf8String.GetInternalNullableOwnedHandleName(),
            _ => Utf8String.GetInternalNonNullableOwnedHandleName(),
        };

        return new RenderableReturnType(nullableTypeName);
    }
}
