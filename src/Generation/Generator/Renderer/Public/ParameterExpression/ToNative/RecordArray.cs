using System;
using Generator.Model;

namespace Generator.Renderer.Public.ParameterExpressions.ToNative;

internal class RecordArray : ParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.IsArray<GirModel.Record>();

    public string? GetExpression(GirModel.Parameter parameter, out string variableName)
    {
        var arrayType = parameter.AnyType.AsT1;

        if (!arrayType.IsPointer)
            throw new NotImplementedException($"{parameter.AnyType}: Not pointed array record types can not yet be converted to native.");

        variableName = Parameter.GetConvertedName(parameter);
        return $"var {variableName} = {Parameter.GetName(parameter)}.Select(record => record.Handle.DangerousGethandle()).ToArray();";
    }
}
