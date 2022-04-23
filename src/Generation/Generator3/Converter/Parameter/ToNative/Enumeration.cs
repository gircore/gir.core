using System;
using GirModel;

namespace Generator3.Converter.Parameter.ToNative;

internal class Enumeration : ParameterConverter
{
    public bool Supports(AnyType type)
        => type.Is<GirModel.Enumeration>();

    public string? GetExpression(GirModel.Parameter parameter, out string variableName)
    {
        if (parameter.Direction != Direction.In)
            throw new NotImplementedException($"{parameter.AnyType}: Enumeration with direction != in not yet supported");

        //We don't need any conversion for enumerations
        variableName = parameter.GetPublicName();
        return null;
    }
}
