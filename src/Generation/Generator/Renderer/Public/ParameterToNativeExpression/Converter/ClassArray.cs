using System;
using System.Collections.Generic;
using Generator.Model;

namespace Generator.Renderer.Public.ParameterToNativeExpressions;

internal class ClassArray : ToNativeParameterConverter
{
    public bool Supports(GirModel.AnyTypeReference anyTypeReference)
        => anyTypeReference.ReferencesArray<GirModel.Class>();

    public void Initialize(ParameterToNativeData parameter, IEnumerable<ParameterToNativeData> _)
    {
        var arrayTypeReference = parameter.Parameter.AnyTypeReferenceOrVarArgs.AsT0.AsT1;

        if (arrayTypeReference.IsPointer)
            throw new NotImplementedException($"{parameter.Parameter.AnyTypeReferenceOrVarArgs}: Pointed class array can not yet be converted to native.");

        var parameterName = Model.Parameter.GetName(parameter.Parameter);
        var nativeVariableName = parameterName + "Native";

        parameter.SetSignatureName(() => parameterName);
        parameter.SetCallName(() => nativeVariableName);
        parameter.SetExpression(() => $"var {nativeVariableName} = {parameterName}.Select(cls => cls.Handle.DangerousGetHandle()).ToArray();");
    }
}
