using System;
using System.Collections.Generic;
using Generator.Model;

namespace Generator.Renderer.Public.ParameterToNativeExpressions;

internal class Record : ToNativeParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.Record>();

    public void Initialize(ParameterToNativeData parameter, IEnumerable<ParameterToNativeData> _)
    {
        if (Model.Parameter.IsGLibError(parameter.Parameter))
            ErrorRecord(parameter);
        else
            RegularRecord(parameter);
    }

    private static void ErrorRecord(ParameterToNativeData parameterData)
    {
        parameterData.IsGLibErrorParameter = true;

        var name = Model.Parameter.GetName(parameterData.Parameter);
        parameterData.SetSignatureName(name);
        parameterData.SetCallName($"out var {name}");
    }

    private static void RegularRecord(ParameterToNativeData parameter)
    {
        if (parameter.Parameter.Direction != GirModel.Direction.In)
            throw new NotImplementedException($"{parameter.Parameter.AnyTypeOrVarArgs}: record parameter '{parameter.Parameter.Name}' with direction != in not yet supported");

        if (!parameter.Parameter.IsPointer)
            throw new NotImplementedException($"{parameter.Parameter.AnyTypeOrVarArgs}: Not pointed record types can not yet be converted to native");

        var parameterName = Model.Parameter.GetName(parameter.Parameter);
        var variableName = parameter.Parameter.Nullable
            ? parameterName + "?.Handle ?? " + Model.Record.GetFullyQualifiedInternalNullHandleInstance((GirModel.Record) parameter.Parameter.AnyTypeOrVarArgs.AsT0.AsT0)
            : parameterName + ".Handle";

        parameter.SetSignatureName(parameterName);
        parameter.SetCallName(variableName);
    }
}
