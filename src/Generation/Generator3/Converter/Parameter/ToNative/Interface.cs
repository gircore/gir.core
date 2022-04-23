using System;
using GirModel;

namespace Generator3.Converter.Parameter.ToNative;

internal class Interface : ParameterConverter
{
    public bool Supports(AnyType type)
        => type.Is<GirModel.Interface>();

    public string? GetExpression(GirModel.Parameter parameter, out string variableName)
    {
        if (parameter.Direction != Direction.In)
            throw new NotImplementedException($"{parameter.AnyType}: Interface parameter with direction != in not yet supported");

        variableName = parameter.GetConvertedName();
        return $"var {variableName} = ({parameter.GetPublicName()} as GObject.Object).Handle;";
    }
}
