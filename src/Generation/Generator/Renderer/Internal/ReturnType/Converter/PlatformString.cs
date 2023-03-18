namespace Generator.Renderer.Internal.ReturnType;

internal class PlatformString : ReturnTypeConverter
{
    public bool Supports(GirModel.ReturnType returnType)
    {
        return returnType.AnyType.Is<GirModel.PlatformString>();
    }

    public RenderableReturnType Convert(GirModel.ReturnType returnType)
    {
        var nullableTypeName = returnType switch
        {
            { Nullable: true, Transfer: GirModel.Transfer.None } => Model.PlatformString.GetInternalNullableUnownedHandleName(),
            { Nullable: false, Transfer: GirModel.Transfer.None } => Model.PlatformString.GetInternalNonNullableUnownedHandleName(),
            { Nullable: true, Transfer: GirModel.Transfer.Full } => Model.PlatformString.GetInternalNullableOwnedHandleName(),
            _ => Model.PlatformString.GetInternalNonNullableOwnedHandleName(),
        };

        return new RenderableReturnType(nullableTypeName);
    }
}
