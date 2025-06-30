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
            return SizeBasedArray(returnType);

        throw new Exception("Unknown kind of array");
    }

    private static RenderableReturnType NullTerminatedArray(GirModel.ReturnType returnType)
    {
        var typeName = returnType switch
        {
            { Transfer: GirModel.Transfer.Full } => Model.Utf8StringArray.NullTerminated.GetInternalOwnedHandleName(),
            { Transfer: GirModel.Transfer.None } => Model.Utf8StringArray.NullTerminated.GetInternalUnownedHandleName(),
            { Transfer: GirModel.Transfer.Container } => Model.Utf8StringArray.NullTerminated.GetInternalContainerHandleName(),
            _ => throw new Exception("Unknown transfer type for utf8 string array return value")
        };

        return new RenderableReturnType(typeName);
    }

    private static RenderableReturnType SizeBasedArray(GirModel.ReturnType returnType)
    {
        var typeName = returnType switch
        {
            { Transfer: GirModel.Transfer.Full } => Model.Utf8StringArray.Sized.GetInternalOwnedHandleName(),
            { Transfer: GirModel.Transfer.None } => Model.Utf8StringArray.Sized.GetInternalUnownedHandleName(),
            { Transfer: GirModel.Transfer.Container } => Model.Utf8StringArray.Sized.GetInternalContainerHandleName(),
            _ => throw new Exception("Unknown transfer type for size based utf8 string array return value")
        };

        return new RenderableReturnType(typeName);
    }
}
