using System;
using System.Collections.Generic;

namespace Generator.Renderer.Public.ParameterToNativeExpressions;

internal class PrimitiveValueTypeAlias : ToNativeParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.IsAlias<GirModel.PrimitiveValueType>();

    public void Initialize(ParameterToNativeData parameter, IEnumerable<ParameterToNativeData> _)
    {
        // When there is a pointer type with direction=in, the native functions are often expecting
        // the pointer to be an array, e.g. gdk_pango_layout_get_clip_region(), so in general this
        // is not safe to generate bindings for.
        if (parameter.Parameter is { IsPointer: true, Direction: GirModel.Direction.In })
            throw new NotImplementedException($"{parameter.Parameter.AnyTypeOrVarArgs}: Pointed primitive value types with direction == in can not yet be converted to native");

        var direcion = parameter.Parameter.Direction;
        switch (direcion)
        {
            case GirModel.Direction.In:
                In(parameter);
                break;
            case GirModel.Direction.InOut:
                InOut(parameter);
                break;
            case GirModel.Direction.Out:
                Out(parameter);
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

    private static void InOut(ParameterToNativeData parameter)
    {
        var alias = (GirModel.Alias) parameter.Parameter.AnyTypeOrVarArgs.AsT0.AsT0;
        var direction = GetDirection(parameter.Parameter);
        var parameterName = Model.Parameter.GetName(parameter.Parameter);
        var parameterNameNative = parameterName + "Native";

        parameter.SetSignatureName(parameterName);
        parameter.SetExpression($"var {parameterNameNative} = ({Model.Type.GetName(alias.Type)}) {parameterName};");
        parameter.SetCallName(direction + parameterNameNative);
        parameter.SetPostCallExpression($"{parameterName} = new {Model.Namespace.GetPublicName(alias.Namespace)}.{alias.Name}({parameterNameNative});");
    }

    private static void Out(ParameterToNativeData parameter)
    {
        var alias = (GirModel.Alias) parameter.Parameter.AnyTypeOrVarArgs.AsT0.AsT0;
        var direction = GetDirection(parameter.Parameter);

        var parameterName = Model.Parameter.GetName(parameter.Parameter);
        var parameterNameNative = parameterName + "Native";

        parameter.SetSignatureName(parameterName);
        parameter.SetCallName($"{direction}var {parameterNameNative}");
        parameter.SetPostCallExpression($"{parameterName} = new {Model.Namespace.GetPublicName(alias.Namespace)}.{alias.Name}({parameterNameNative});");
    }

    private static string GetDirection(GirModel.Parameter parameter) => parameter switch
    {
        { Direction: GirModel.Direction.InOut } => ParameterDirection.Ref(),
        { Direction: GirModel.Direction.Out } => ParameterDirection.Out(),
        _ => ParameterDirection.In()
    };
}
