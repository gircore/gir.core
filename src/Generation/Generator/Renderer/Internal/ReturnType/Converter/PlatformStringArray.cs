using System;

namespace Generator.Renderer.Internal.ReturnType;

internal class PlatformStringArray : ReturnTypeConverter
{
    public bool Supports(GirModel.ReturnType returnType)
    {
        return returnType.AnyType.IsArray<GirModel.PlatformString>();
    }

    public RenderableReturnType Convert(GirModel.ReturnType returnType)
    {
        var arrayType = returnType.AnyType.AsT1;

        if (arrayType.IsZeroTerminated)
            return NullTerminatedArray(returnType);

        if (arrayType.Length is not null)
            return SizeBasedArray(returnType);

        throw new Exception("Unknown kind of array");
    }

    private static RenderableReturnType NullTerminatedArray(GirModel.ReturnType returnType)
    {
        var typeName = returnType switch
        {
            { Transfer: GirModel.Transfer.Full } => Model.PlatformStringArray.NullTerminated.GetInternalOwnedHandleName(),
            { Transfer: GirModel.Transfer.None } => Model.PlatformStringArray.NullTerminated.GetInternalUnownedHandleName(),
            { Transfer: GirModel.Transfer.Container } => Model.PlatformStringArray.NullTerminated.GetInternalContainerHandleName(),
            _ => throw new Exception("Unknown transfer type for null terminated platform string array return value")
        };

        return new RenderableReturnType(typeName);
    }

    private static RenderableReturnType SizeBasedArray(GirModel.ReturnType returnType)
    {
        var typeName = returnType switch
        {
            { Transfer: GirModel.Transfer.Full } => Model.PlatformStringArray.Sized.GetInternalOwnedHandleName(),
            { Transfer: GirModel.Transfer.None } => Model.PlatformStringArray.Sized.GetInternalUnownedHandleName(),
            { Transfer: GirModel.Transfer.Container } => Model.PlatformStringArray.Sized.GetInternalContainerHandleName(),
            _ => throw new Exception("Unknown transfer type for size based platform string array return value")
        };

        return new RenderableReturnType(typeName);
    }
}
