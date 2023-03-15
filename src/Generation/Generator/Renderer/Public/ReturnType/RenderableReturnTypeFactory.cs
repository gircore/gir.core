using System.Collections.Generic;

namespace Generator.Renderer.Public.ReturnType;

internal static class RenderableReturnTypeFactory
{
    private static readonly List<ReturnTypeConverter> converters = new()
    {
        new Bitfield(),
        new Class(),
        new Enumeration(),
        new Interface(),
        new Pointer(),
        new PrimitiveValue(),
        new Record(),
        new String(),
        new Void(),
        new RecordArray(),
        new StringArray(),
    };

    public static RenderableReturnType Create(GirModel.ReturnType returnValue)
    {
        foreach (var converter in converters)
            if (converter.Supports(returnValue))
                return converter.Create(returnValue);

        throw new System.NotImplementedException($"Missing converter for return type {returnValue.AnyType}.");
    }
}
