using System;
using System.Collections.Generic;

namespace Generator.Renderer.Public.ParameterToNativeExpressions;

internal class Long : ToNativeParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.Long>();

    public void Initialize(ParameterToNativeData parameter, IEnumerable<ParameterToNativeData> _)
    {
        //Array length parameters are handled as part of the corresponding array converters
        if (parameter.IsArrayLengthParameter)
            return;

        switch (parameter.Parameter)
        {
            case { IsPointer: true, Direction: GirModel.Direction.In }:
                Ref(parameter);
                break;
            case { IsPointer: false, Direction: GirModel.Direction.In }:
                Direct(parameter);
                break;
            case { Direction: GirModel.Direction.InOut }:
                Ref(parameter);
                break;
            case { Direction: GirModel.Direction.Out }:
                Out(parameter);
                break;
            default:
                throw new NotImplementedException($"{parameter.Parameter.AnyTypeOrVarArgs}: This primitive value types can not yet be converted to native");
        }
    }

    private static void Direct(ParameterToNativeData parameter)
    {
        var parameterName = Model.Parameter.GetName(parameter.Parameter);
        parameter.SetSignatureName(() => parameterName);
        parameter.SetCallName(() => parameterName);
    }

    private static void Ref(ParameterToNativeData parameter)
    {
        var parameterName = Model.Parameter.GetName(parameter.Parameter);
        parameter.SetSignatureName(() => parameterName);
        parameter.SetCallName(() => $"ref {parameterName}");
    }

    private static void Out(ParameterToNativeData parameter)
    {
        var parameterName = Model.Parameter.GetName(parameter.Parameter);
        parameter.SetSignatureName(() => parameterName);
        parameter.SetCallName(() => parameter.IsArrayLengthParameter
            ? $"out var {parameterName}"
            : $"out {parameterName}");
    }
}
