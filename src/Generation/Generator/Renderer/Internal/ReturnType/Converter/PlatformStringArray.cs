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
        // var arrayType = returnType.AnyType.AsT1;

        // if (arrayType.IsZeroTerminated)
            return NullTerminatedArray(returnType);

        // if (arrayType.Length is not null)
        //     return SizeBasedArray();

        // throw new Exception("Unknown kind of array");
    }

    private static RenderableReturnType NullTerminatedArray(GirModel.ReturnType returnType)
    {
        var typeName = returnType switch
        {
            { Transfer: GirModel.Transfer.Full } => Model.PlatformStringArray.GetInternalOwnedHandleName(),
            { Transfer: GirModel.Transfer.None } => Model.PlatformStringArray.GetInternalUnownedHandleName(),
            { Transfer: GirModel.Transfer.Container } => Model.PlatformStringArray.GetInternalContainerHandleName(),
            _ => throw new Exception("Unknown transfer type for platform string array return value")
        };

        return new RenderableReturnType(typeName);
    }

    private static RenderableReturnType SizeBasedArray()
    {
        return new RenderableReturnType("string[]");
    }
}
