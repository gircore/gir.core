using System;
using Generator.Model;

namespace Generator.Renderer.Public.ParameterExpressions.ToNative;

internal class Class : ParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.Class>();

    public string? GetExpression(GirModel.Parameter parameter, out string variableName)
    {
        if (parameter.Direction != GirModel.Direction.In)
            throw new NotImplementedException($"{parameter.AnyType}: class parameter with direction != in not yet supported");

        if (!parameter.IsPointer)
            throw new NotImplementedException($"{parameter.AnyType}: class parameter which is no pointer can not be converted to native");

        if (parameter.Nullable)
            variableName = Parameter.GetName(parameter) + "?.Handle ?? IntPtr.Zero";
        else
            variableName = Parameter.GetName(parameter) + ".Handle";

        return null;
    }
}
