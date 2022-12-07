using System;
using System.Collections.Generic;
using Generator.Model;

namespace Generator.Renderer.Public.ParameterToNativeExpressions;

internal class Class : ToNativeParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.Class>();

    public void Initialize(ParameterToNativeData parameter, IEnumerable<ParameterToNativeData> _)
    {
        if (parameter.Parameter.Direction != GirModel.Direction.In)
            throw new NotImplementedException($"{parameter.Parameter.AnyType}: class parameter with direction != in not yet supported");

        if (!parameter.Parameter.IsPointer)
            throw new NotImplementedException($"{parameter.Parameter.AnyType}: class parameter which is no pointer can not be converted to native");

        var parameterName = Parameter.GetName(parameter.Parameter);
        var callParameter = parameter.Parameter.Nullable
            ? parameterName + "?.Handle ?? IntPtr.Zero"
            : parameterName + ".Handle";

        parameter.SetSignatureName(parameterName);
        parameter.SetCallName(callParameter);
    }
}
