using System;
using System.Collections.Generic;

namespace Generator.Renderer.Public.ParameterToNativeExpressions;

internal class CLong : ToNativeParameterConverter
{
    public bool Supports(GirModel.AnyTypeReference anyTypeReference)
        => anyTypeReference.References<GirModel.CLong>();

    public void Initialize(ParameterToNativeData parameter, IEnumerable<ParameterToNativeData> _)
    {
        //Array length parameters are handled as part of the corresponding array converters
        if (parameter.IsArrayLengthParameter)
            return;

        switch (parameter.Parameter)
        {
            case { IsPointer: false, Direction: GirModel.Direction.In }:
                Direct(parameter);
                break;
            default:
                throw new NotImplementedException($"{parameter.Parameter.AnyTypeReferenceOrVarArgs}: This long value type can not yet be converted to native");
        }
    }

    private static void Direct(ParameterToNativeData parameter)
    {
        var parameterName = Model.Parameter.GetName(parameter.Parameter);
        parameter.SetSignatureName(() => parameterName);
        parameter.SetCallName(() => $"new CLong(checked((nint) {parameterName}))");
    }
}
