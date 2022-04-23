using System;

namespace Generator3.Converter.Parameter.ToManaged;

internal class Enumeration : ParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.Enumeration>();

    public string? GetExpression(GirModel.Parameter parameter, out string variableName)
    {
        if (parameter.Direction != GirModel.Direction.In)
            throw new NotImplementedException($"{parameter.AnyType}: Enumeration with direction != in not yet supported");

        //We don't need any conversion for enumerations
        variableName = parameter.GetPublicName();
        return null;
    }
}
