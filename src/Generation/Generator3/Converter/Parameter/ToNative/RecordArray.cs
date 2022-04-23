using System;
using GirModel;

namespace Generator3.Converter.Parameter.ToNative;

internal class RecordArray : ParameterConverter
{
    public bool Supports(AnyType type)
        => type.IsArray<GirModel.Record>();

    public string? GetExpression(GirModel.Parameter parameter, out string variableName)
    {
        var arrayType = parameter.AnyType.AsT1;

        if (!arrayType.IsPointer)
            throw new NotImplementedException($"{parameter.AnyType}: Not pointed array record types can not yet be converted to native.");

        variableName = parameter.GetConvertedName();
        return $"var {variableName} = {parameter.GetPublicName()}.Select(record => record.Handle.DangerousGethandle()).ToArray();";
    }
}
