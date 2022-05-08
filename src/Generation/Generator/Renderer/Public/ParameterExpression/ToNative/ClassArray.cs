using System;
using Generator.Model;

namespace Generator.Renderer.Public.ParameterExpressions.ToNative;

internal class ClassArray : ParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.IsArray<GirModel.Class>();

    public string GetExpression(GirModel.Parameter parameter, out string variableName)
    {
        var arrayType = parameter.AnyType.AsT1;

        if (arrayType.IsPointer)
            throw new NotImplementedException($"{parameter.AnyType}: Pointed class array can not yet be converted to native.");

        variableName = Parameter.GetConvertedName(parameter);
        return $"var {variableName} = {Parameter.GetName(parameter)}.Select(cls => cls.Handle).ToArray();";
    }
}
