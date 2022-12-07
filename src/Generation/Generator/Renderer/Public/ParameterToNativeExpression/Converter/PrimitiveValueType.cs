using System;
using System.Collections.Generic;
using Generator.Model;

namespace Generator.Renderer.Public.ParameterToNativeExpressions;

internal class PrimitiveValueType : ToNativeParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.PrimitiveValueType>();

    public void Initialize(ParameterToNativeData parameter, IEnumerable<ParameterToNativeData> _)
    {
        if (parameter.Parameter.IsPointer)
            throw new NotImplementedException($"{parameter.Parameter.AnyType}: Pointed primitive value types can not yet be converted to native");

        if (parameter.Parameter.Direction != GirModel.Direction.In)
            throw new NotImplementedException($"{parameter.Parameter.AnyType}: Primitive value type with direction != in not yet supported");

        //We don't need any conversion for native parameters
        var parameterName = Parameter.GetName(parameter.Parameter);
        parameter.SetSignatureName(parameterName);
        parameter.SetCallName(parameterName);
    }
}
