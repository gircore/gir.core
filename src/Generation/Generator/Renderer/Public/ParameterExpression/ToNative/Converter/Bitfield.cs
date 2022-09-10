using System;
using System.Collections.Generic;
using Generator.Model;

namespace Generator.Renderer.Public.ParameterExpressions.ToNative;

internal class Bitfield : ToNativeParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.Bitfield>();

    public void Initialize(ParameterToNativeData parameterData, IEnumerable<ParameterToNativeData> _)
    {
        if (parameterData.Parameter.Direction != GirModel.Direction.In)
            throw new NotImplementedException($"{parameterData.Parameter.AnyType}: Bitfield with direction != in not yet supported");

        var parameterName = Parameter.GetName(parameterData.Parameter);
        parameterData.SetSignatureName(parameterName);
        parameterData.SetCallName(parameterName);
    }
}
