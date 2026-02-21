using System;
using System.Collections.Generic;

namespace Generator.Renderer.Public.ParameterToNativeExpressions;

internal class Enumeration : ToNativeParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.Enumeration>();

    public void Initialize(ParameterToNativeData parameter, IEnumerable<ParameterToNativeData> _)
    {
        switch (parameter.Parameter)
        {
            case { Direction: GirModel.Direction.In, IsPointer: false }:
                Direct(parameter);
                break;
            case { Direction: GirModel.Direction.InOut, IsPointer: true }:
            case { Direction: GirModel.Direction.Out, IsPointer: true, CallerAllocates: true }:
                Ref(parameter);
                break;
            case { Direction: GirModel.Direction.Out, IsPointer: true }:
                Out(parameter);
                break;
            default:
                throw new NotImplementedException($"{parameter.Parameter.AnyTypeOrVarArgs}: This kind of enumeration (pointed: {parameter.Parameter.IsPointer}, direction: {parameter.Parameter.Direction} can't be converted to managed currently.");
        }
    }

    private static void Ref(ParameterToNativeData parameterData)
    {
        var variableName = Model.Parameter.GetName(parameterData.Parameter);

        parameterData.SetSignatureName(() => variableName);
        parameterData.SetCallName(() => $"ref {variableName}");
    }

    private static void Direct(ParameterToNativeData parameterData)
    {
        var variableName = Model.Parameter.GetName(parameterData.Parameter);

        parameterData.SetSignatureName(() => variableName);
        parameterData.SetCallName(() => variableName);
    }

    private static void Out(ParameterToNativeData parameterData)
    {
        var variableName = Model.Parameter.GetName(parameterData.Parameter);

        parameterData.SetSignatureName(() => variableName);
        parameterData.SetCallName(() => $"out {variableName}");
    }
}
