using System;
using GirModel;

namespace Generator3.Converter.Parameter.ToNative;

internal class ClassArray : ParameterConverter
{
    public bool Supports(AnyType type)
        => type.IsArray<GirModel.Class>();

    public string GetExpression(GirModel.Parameter parameter, out string variableName)
    {
        var arrayType = parameter.AnyType.AsT1;

        if (arrayType.IsPointer)
            throw new NotImplementedException($"{parameter.AnyType}: Pointed class array can not yet be converted to native.");

        variableName = parameter.GetConvertedName();
        return $"var {variableName} = {parameter.GetPublicName()}.Select(cls => cls.Handle).ToArray();";
    }
}
