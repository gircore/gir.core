using System;
using GirModel;

namespace Generator3.Converter.Parameter.ToNative;

internal class Bitfield : ParameterConverter
{
    public bool Supports(AnyType type)
        => type.Is<GirModel.Bitfield>();

    public string? GetExpression(GirModel.Parameter parameter, out string variableName)
    {
        if (parameter.Direction != Direction.In)
            throw new NotImplementedException($"{parameter.AnyType}: Bitfield with direction != in not yet supported");

        //We don't need any conversion for bitfields
        variableName = parameter.GetPublicName();
        return null;
    }
}
