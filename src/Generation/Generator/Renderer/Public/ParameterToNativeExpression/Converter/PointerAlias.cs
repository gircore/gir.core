using System;
using System.Collections.Generic;

namespace Generator.Renderer.Public.ParameterToNativeExpressions;

internal class PointerAlias : ToNativeParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.IsAlias<GirModel.Pointer>();

    public void Initialize(ParameterToNativeData parameter, IEnumerable<ParameterToNativeData> _)
    {
        var direcion = parameter.Parameter.Direction;
        switch (direcion)
        {
            case GirModel.Direction.In:
                In(parameter);
                break;
            case GirModel.Direction.InOut:
                throw new Exception("PointerAlias: InOut not yet implemented");
                break;
            case GirModel.Direction.Out:
                throw new Exception("PointerAlias: Out not yet implemented");
                break;
        }
    }

    private static void In(ParameterToNativeData parameter)
    {
        var direction = GetDirection(parameter.Parameter);
        var parameterName = Model.Parameter.GetName(parameter.Parameter);

        parameter.SetSignatureName(parameterName);
        parameter.SetCallName(direction + parameterName);
    }

    private static string GetDirection(GirModel.Parameter parameter) => parameter switch
    {
        { Direction: GirModel.Direction.InOut } => ParameterDirection.Ref(),
        { Direction: GirModel.Direction.Out } => ParameterDirection.Out(),
        _ => ParameterDirection.In()
    };
}
