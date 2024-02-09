using System;
using System.Collections.Generic;
using System.Linq;
using Generator.Model;

namespace Generator.Renderer.Public.ParameterToNativeExpressions;

internal class Callback : ToNativeParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.Callback>();

    public void Initialize(ParameterToNativeData parameter, IEnumerable<ParameterToNativeData> parameters)
    {
        if (parameter.Parameter.Direction != GirModel.Direction.In)
            throw new NotImplementedException($"{parameter.Parameter.AnyTypeOrVarArgs}: Callback parameter with direction != in not yet supported");

        if (parameter is { HasCallName: true, HasSignatureName: true })
            return; //If this parameter got already initialized by another parameter we can skip it as it is not part of the public api

        switch (parameter.Parameter.Scope)
        {
            case GirModel.Scope.Call:
                FillCallScope(parameter, parameters);
                break;
            case GirModel.Scope.Async:
                throw new NotImplementedException($"{parameter.Parameter.AnyTypeOrVarArgs}: Async scope not yet implemented");
            case GirModel.Scope.Notified:
                FillNotifiedScope(parameter, parameters);
                break;
            case GirModel.Scope.Forever:
                throw new NotImplementedException($"{parameter.Parameter.AnyTypeOrVarArgs}: Forever scope not yet implemented");
            default:
                throw new Exception($"Unknown parameter scope {parameter.Parameter.Scope}");
        }
    }

    private static void FillNotifiedScope(ParameterToNativeData parameter, IEnumerable<ParameterToNativeData> parameters)
    {
        var callback = (GirModel.Callback) parameter.Parameter.AnyTypeOrVarArgs.AsT0.AsT0;
        var parameterName = Model.Parameter.GetName(parameter.Parameter);
        var handlerNameVariable = parameterName + "Handler";

        if (parameter.Parameter.Destroy is null)
            throw new Exception($"{parameter.Parameter.AnyTypeOrVarArgs}: Notified scope misses destroy index");

        if (parameter.Parameter.Closure is { } closureIndex)
            parameters.ElementAt(closureIndex).IsCallbackUserData = true;

        parameter.SetSignatureName(() => parameterName);
        parameter.SetCallName(() => handlerNameVariable + ".NativeCallback");

        var destroyParameter = parameters.ElementAt(parameter.Parameter.Destroy.Value);

        if (destroyParameter.Parameter.AnyTypeOrVarArgs.AsT0.AsT0 is not GirModel.Callback { Name: "DestroyNotify" })
            throw new Exception("Destroyparameter is not of type DestroyNotify");

        destroyParameter.IsDestroyNotify = true;
        destroyParameter.SetSignatureName(() => "destroy");
        destroyParameter.SetCallName(() => handlerNameVariable + ".DestroyNotify");

        parameter.SetExpression(() => $"var {handlerNameVariable} = new {Namespace.GetInternalName(callback.Namespace)}.{Model.Callback.GetNotifiedHandlerName(callback)}({parameterName});");
    }

    private static void FillCallScope(ParameterToNativeData parameter, IEnumerable<ParameterToNativeData> parameters)
    {
        var callback = (GirModel.Callback) parameter.Parameter.AnyTypeOrVarArgs.AsT0.AsT0;
        var parameterName = Model.Parameter.GetName(parameter.Parameter);
        var handlerNameVariable = parameterName + "Handler";

        parameter.SetSignatureName(() => parameterName);
        parameter.SetCallName(() => handlerNameVariable + ".NativeCallback");
        parameter.SetExpression(() => $"var {handlerNameVariable} = new {Namespace.GetInternalName(callback.Namespace)}.{Model.Callback.GetCallHandlerName(callback)}({parameterName});");
    }
}
