using System;

namespace Generator.Renderer.Internal.ReturnType;

internal class Utf8StringArray : ReturnTypeConverter
{
    public bool Supports(GirModel.ReturnType returnType)
    {
        return returnType.AnyType.IsArray<GirModel.Utf8String>();
    }

    public RenderableReturnType Convert(GirModel.ReturnType returnType)
    {
        var arrayType = returnType.AnyType.AsT1;

        if (arrayType.IsZeroTerminated)
            return NullTerminatedArray(returnType);

        if (arrayType.Length is not null)
            return SizeBasedArray();

        throw new Exception("Unknown kind of array");
    }

    private static RenderableReturnType NullTerminatedArray(GirModel.ReturnType returnType)
    {
        var typeName = returnType switch
        {
            { Transfer: GirModel.Transfer.Full } => Model.Utf8StringArray.GetInternalOwnedHandleName(),
            { Transfer: GirModel.Transfer.None } => Model.Utf8StringArray.GetInternalUnownedHandleName(),
            { Transfer: GirModel.Transfer.Container } => Model.Utf8StringArray.GetInternalContainerHandleName(),
            _ => throw new Exception("Unknown transfer type for utf8 string array return value")
        };

        return new RenderableReturnType(typeName);
    }

    private static RenderableReturnType SizeBasedArray()
    {
        return new RenderableReturnType("string[]");
    }
}
