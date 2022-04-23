using System;
using GirModel;

namespace Generator3.Converter.Parameter.ToNative;

internal class Class : ParameterConverter
{
    public bool Supports(AnyType type)
        => type.Is<GirModel.Class>();

    public string? GetExpression(GirModel.Parameter parameter, out string variableName)
    {
        if (parameter.Direction != Direction.In)
            throw new NotImplementedException($"{parameter.AnyType}: class parameter with direction != in not yet supported");

        if (!parameter.IsPointer)
            throw new NotImplementedException($"{parameter.AnyType}: class parameter which is no pointer can not be converted to native");

        if (parameter.Nullable)
            variableName = parameter.GetPublicName() + "?.Handle ?? IntPtr.Zero";
        else
            variableName = parameter.GetPublicName() + ".Handle";

        return null;
    }
}
