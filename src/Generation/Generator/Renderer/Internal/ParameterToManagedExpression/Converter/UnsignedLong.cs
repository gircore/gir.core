using System;
using System.Collections.Generic;

namespace Generator.Renderer.Internal.ParameterToManagedExpressions;

internal class UnsignedLong : ToManagedParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.UnsignedLong>();

    public void Initialize(ParameterToManagedData parameterData, IEnumerable<ParameterToManagedData> parameters)
    {
        switch (parameterData.Parameter)
        {
            case { Direction: GirModel.Direction.In, IsPointer: true }:
                Ref(parameterData);
                break;
            case { Direction: GirModel.Direction.In, IsPointer: false }:
                Direct(parameterData);
                break;
            case { Direction: GirModel.Direction.Out, IsPointer: true }:
                Out(parameterData);
                break;
            default:
                throw new NotImplementedException($"This kind of internal primitive value type (pointed: {parameterData.Parameter.IsPointer}, direction: {parameterData.Parameter.Direction} can't be converted to managed currently.");
        }
    }

    private static void Ref(ParameterToManagedData parameterData)
    {
        var variableName = Model.Parameter.GetName(parameterData.Parameter);

        parameterData.SetSignatureName(() => variableName);
        parameterData.SetCallName(() => $"ref {variableName}");
    }

    private static void Direct(ParameterToManagedData parameterData)
    {
        var variableName = Model.Parameter.GetName(parameterData.Parameter);

        parameterData.SetSignatureName(() => variableName);
        parameterData.SetCallName(() => variableName);
    }

    private static void Out(ParameterToManagedData parameterData)
    {
        var variableName = Model.Parameter.GetName(parameterData.Parameter);

        parameterData.SetSignatureName(() => variableName);
        parameterData.SetCallName(() => $"out {variableName}");
    }
}
