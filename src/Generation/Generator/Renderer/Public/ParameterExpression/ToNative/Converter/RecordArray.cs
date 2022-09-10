using System;
using System.Collections.Generic;
using Generator.Model;

namespace Generator.Renderer.Public.ParameterExpressions.ToNative;

internal class RecordArray : ToNativeParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.IsArray<GirModel.Record>();

    public void Initialize(ParameterToNativeData parameter, IEnumerable<ParameterToNativeData> _)
    {
        var arrayType = parameter.Parameter.AnyType.AsT1;

        if (!arrayType.IsPointer)
            throw new NotImplementedException($"{parameter.Parameter.AnyType}: Not pointed array record types can not yet be converted to native.");

        var parameterName = Parameter.GetName(parameter.Parameter);
        var nativeVariableName = parameterName + "Native";

        parameter.SetSignatureName(parameterName);
        parameter.SetCallName(nativeVariableName);
        parameter.SetExpression($"var {nativeVariableName} = {parameterName}.Select(record => record.Handle.DangerousGethandle()).ToArray();");
    }
}
