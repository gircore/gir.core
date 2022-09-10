using System;
using System.Collections.Generic;
using System.Linq;
using Generator.Model;

namespace Generator.Renderer.Public.ParameterExpressions.ToNative;

internal class Callback : ToNativeParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.Callback>();

    public void Initialize(ParameterToNativeData parameter, IEnumerable<ParameterToNativeData> parameters)
    {
        if (parameter.Parameter.Direction != GirModel.Direction.In)
            throw new NotImplementedException($"{parameter.Parameter.AnyType}: Callback parameter with direction != in not yet supported");

        if (parameter.HasCallName && parameter.HasSignatureName)
            return; //If this parameter got already initialized by another parameter we can skip it as it is not part of the public api

        switch (parameter.Parameter.Scope)
        {
            case GirModel.Scope.Call:
                throw new NotImplementedException($"{parameter.Parameter.AnyType}: Call scope not yet implemented");
            case GirModel.Scope.Async:
                throw new NotImplementedException($"{parameter.Parameter.AnyType}: Async scope not yet implemented");
            case GirModel.Scope.Notified:
                FillNotifiedScope(parameter, parameters);
                break;
            case GirModel.Scope.Forever:
                throw new NotImplementedException($"{parameter.Parameter.AnyType}: Forever scope not yet implemented");
            default:
                throw new Exception($"Unknown parameter scope {parameter.Parameter.Scope}");
        }
    }

    private static void FillNotifiedScope(ParameterToNativeData parameter, IEnumerable<ParameterToNativeData> parameters)
    {
        var callback = (GirModel.Callback) parameter.Parameter.AnyType.AsT0;
        var parameterName = Parameter.GetName(parameter.Parameter);
        var handlerNameVariable = parameterName + "Handler";

        if (parameter.Parameter.Destroy is null)
            throw new Exception($"{parameter.Parameter.AnyType}: Notified scope misses destroy index");

        if (parameter.Parameter.Closure is { } closureIndex)
            parameters.ElementAt(closureIndex).IsClosure = true;

        parameter.SetSignatureName(parameterName);
        parameter.SetCallName(handlerNameVariable + ".NativeCallback");

        var destroyParameter = parameters.ElementAt(parameter.Parameter.Destroy.Value);

        if (destroyParameter.Parameter.AnyType.AsT0 is not GirModel.Callback { Name: "DestroyNotify" })
            throw new Exception("Destroyparameter is not of type DestroyNotify");

        destroyParameter.IsDestroyNotify = true;
        destroyParameter.SetSignatureName("destroy");
        destroyParameter.SetCallName(handlerNameVariable + ".DestroyNotify");

        parameter.SetExpression($"var {handlerNameVariable} = new {Namespace.GetPublicName(callback.Namespace)}.{Model.Callback.GetNotifiedHandlerName(callback)}({parameterName});");
    }
}
