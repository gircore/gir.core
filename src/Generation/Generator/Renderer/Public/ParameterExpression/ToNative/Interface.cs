using System;
using Generator.Model;

namespace Generator.Renderer.Public.ParameterExpressions.ToNative;

internal class Interface : ParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.Interface>();

    public string? GetExpression(GirModel.Parameter parameter, out string variableName)
    {
        if (parameter.Direction != GirModel.Direction.In)
            throw new NotImplementedException($"{parameter.AnyType}: Interface parameter with direction != in not yet supported");

        variableName = Parameter.GetConvertedName(parameter);
        return $"var {variableName} = ({Parameter.GetName(parameter)} as GObject.Object).Handle;";
    }
}
