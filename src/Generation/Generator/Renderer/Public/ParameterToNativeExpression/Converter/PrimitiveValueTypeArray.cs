using System;
using System.Collections.Generic;
using Generator.Model;

namespace Generator.Renderer.Public.ParameterToNativeExpressions;

internal class PrimitiveValueTypeArray : ToNativeParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.IsArray<GirModel.PrimitiveValueType>();

    public void Initialize(ParameterToNativeData parameter, IEnumerable<ParameterToNativeData> _)
    {
        if (parameter.Parameter.IsPointer)
            throw new NotImplementedException($"{parameter.Parameter.AnyTypeOrVarArgs}: Pointed primitive value type array can not yet be converted to native");

        if (parameter.Parameter.Direction != GirModel.Direction.In)
            throw new NotImplementedException($"{parameter.Parameter.AnyTypeOrVarArgs}: Primitive value type array with direction != in not yet supported");

        //We don't need any conversion for native parameters
        var parameterName = Parameter.GetName(parameter.Parameter);
        parameter.SetSignatureName(parameterName);
        parameter.SetCallName(parameterName);
    }
}
