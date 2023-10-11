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

        var nullableTypeName = arrayType.Length is null
            ? NullTerminatedArray(returnType)
            : SizeBasedArray();

        return new RenderableReturnType(nullableTypeName);
    }

    private static string NullTerminatedArray(GirModel.ReturnType returnType)
    {
        return returnType switch
        {
            { Transfer: GirModel.Transfer.Full } => Model.Utf8StringArray.GetInternalOwnedHandleName(),
            { Transfer: GirModel.Transfer.None } => Model.Utf8StringArray.GetInternalUnownedHandleName(),
            { Transfer: GirModel.Transfer.Container } => Model.Utf8StringArray.GetInternalContainerHandleName(),
            _ => throw new Exception("Unknown transfer type for utf8 string array return value")
        };
    }

    private static string SizeBasedArray()
    {
        return "string[]";
    }
}
