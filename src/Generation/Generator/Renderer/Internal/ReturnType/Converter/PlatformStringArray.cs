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

        var nullableTypeName = arrayType.Length is null
            ? NullTerminatedArray(returnType)
            : SizeBasedArray();

        return new RenderableReturnType(nullableTypeName);
    }

    private static string NullTerminatedArray(GirModel.ReturnType returnType)
    {
        return returnType switch
        {
            { Transfer: GirModel.Transfer.Full } => Model.PlatformStringArray.GetInternalOwnedHandleName(),
            { Transfer: GirModel.Transfer.None } => Model.PlatformStringArray.GetInternalUnownedHandleName(),
            { Transfer: GirModel.Transfer.Container } => Model.PlatformStringArray.GetInternalContainerHandleName(),
            _ => throw new Exception("Unknown transfer type for platform string array return value")
        };
    }

    private static string SizeBasedArray()
    {
        return "string[]";
    }
}
