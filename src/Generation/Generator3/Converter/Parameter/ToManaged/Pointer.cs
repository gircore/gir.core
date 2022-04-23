using System;

namespace Generator3.Converter.Parameter.ToManaged;

internal class Pointer : ParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.Pointer>();

    public string? GetExpression(GirModel.Parameter parameter, out string variableName)
    {
        if (parameter.Direction != GirModel.Direction.In)
            throw new NotImplementedException($"{parameter.AnyType}: Pointer with direction != in not yet supported");

        //We don't need any conversion for bitfields
        variableName = parameter.GetPublicName();
        return null;
    }
}
