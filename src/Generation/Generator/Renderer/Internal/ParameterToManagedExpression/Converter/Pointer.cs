using System;
using System.Collections.Generic;

namespace Generator.Renderer.Internal.ParameterToManagedExpressions;

internal class Pointer : ToManagedParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.Pointer>();

    public void Initialize(ParameterToManagedData parameterData, IEnumerable<ParameterToManagedData> parameters)
    {
        switch (parameterData.Parameter.Direction)
        {
            case GirModel.Direction.In:
                In(parameterData);
                break;
            case GirModel.Direction.Out:
                Out(parameterData);
                break;
            case GirModel.Direction.InOut:
                Ref(parameterData);
                break;
            default:
                throw new Exception("Can't convert parameter to managed. Unknown direction");
        }

        if (parameterData.Parameter.Closure is not null)
            parameterData.IsCallbackUserData = true;
    }

    private static void In(ParameterToManagedData parameterData)
    {
        var variableName = Model.Parameter.GetName(parameterData.Parameter);
        parameterData.SetSignatureName(variableName);
        parameterData.SetCallName(variableName);
    }

    private static void Out(ParameterToManagedData parameterData)
    {
        var variableName = Model.Parameter.GetName(parameterData.Parameter);
        parameterData.SetSignatureName(variableName);
        parameterData.SetCallName($"out {variableName}");
    }

    private static void Ref(ParameterToManagedData parameterData)
    {
        var variableName = Model.Parameter.GetName(parameterData.Parameter);
        parameterData.SetSignatureName(variableName);
        parameterData.SetCallName($"ref {variableName}");
    }
}
