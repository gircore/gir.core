using System;
using System.Collections.Generic;
using Generator.Model;

namespace Generator.Renderer.Public.ParameterToNativeExpressions;

internal class ClassArray : ToNativeParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.IsArray<GirModel.Class>();

    public void Initialize(ParameterToNativeData parameter, IEnumerable<ParameterToNativeData> _)
    {
        var arrayType = parameter.Parameter.AnyTypeOrVarArgs.AsT0.AsT1;

        if (arrayType.IsPointer)
            throw new NotImplementedException($"{parameter.Parameter.AnyTypeOrVarArgs}: Pointed class array can not yet be converted to native.");

        var parameterName = Parameter.GetName(parameter.Parameter);
        var nativeVariableName = parameterName + "Native";

        parameter.SetSignatureName(parameterName);
        parameter.SetCallName(nativeVariableName);
        parameter.SetExpression($"var {nativeVariableName} = {parameterName}.Select(cls => cls.Handle).ToArray();");
    }
}
