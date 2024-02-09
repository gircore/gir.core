using System.Collections.Generic;

namespace Generator.Renderer.Public.ParameterToNativeExpressions;

internal class PrimitiveValueTypeAlias : ToNativeParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.IsAlias<GirModel.PrimitiveValueType>();

    public void Initialize(ParameterToNativeData parameter, IEnumerable<ParameterToNativeData> _)
    {
        switch (parameter.Parameter)
        {
            case { Direction: GirModel.Direction.In, IsPointer: true }:
                InPointer(parameter);
                break;
            case { Direction: GirModel.Direction.In, IsPointer: false }:
                In(parameter);
                break;
            case { Direction: GirModel.Direction.InOut }:
                InOut(parameter);
                break;
            case { Direction: GirModel.Direction.Out }:
                Out(parameter);
                break;
        }
    }

    private static void InPointer(ParameterToNativeData parameter)
    {
        var parameterName = Model.Parameter.GetName(parameter.Parameter);

        parameter.SetSignatureName(() => parameterName);
        parameter.SetCallName(() => ParameterDirection.Ref() + parameterName);
    }

    private static void In(ParameterToNativeData parameter)
    {
        var parameterName = Model.Parameter.GetName(parameter.Parameter);

        parameter.SetSignatureName(() => parameterName);
        parameter.SetCallName(() => ParameterDirection.In() + parameterName);
    }

    private static void InOut(ParameterToNativeData parameter)
    {
        var parameterName = Model.Parameter.GetName(parameter.Parameter);

        parameter.SetSignatureName(() => parameterName);
        parameter.SetCallName(() => ParameterDirection.Ref() + parameterName);
    }

    private static void Out(ParameterToNativeData parameter)
    {
        var parameterName = Model.Parameter.GetName(parameter.Parameter);

        parameter.SetSignatureName(() => parameterName);
        parameter.SetCallName(() => ParameterDirection.Out() + parameterName);
    }
}
