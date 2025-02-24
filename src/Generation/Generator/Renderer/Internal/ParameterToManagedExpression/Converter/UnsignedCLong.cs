using System;
using System.Collections.Generic;

namespace Generator.Renderer.Internal.ParameterToManagedExpressions;

internal class UnsignedCLong : ToManagedParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.UnsignedCLong>();

    public void Initialize(ParameterToManagedData parameterData, IEnumerable<ParameterToManagedData> parameters)
    {
        switch (parameterData.Parameter)
        {
            case { Direction: GirModel.Direction.In, IsPointer: false }:
                Direct(parameterData);
                break;
            default:
                throw new NotImplementedException($"This kind of internal unsigned long value type (pointed: {parameterData.Parameter.IsPointer}, direction: {parameterData.Parameter.Direction} can't be converted to managed currently.");
        }
    }

    private static void Direct(ParameterToManagedData parameterData)
    {
        var variableName = Model.Parameter.GetName(parameterData.Parameter);

        parameterData.SetSignatureName(() => variableName);
        parameterData.SetCallName(() => $"{variableName}.Value");
    }
}
