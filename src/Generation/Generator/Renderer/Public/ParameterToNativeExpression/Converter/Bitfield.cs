using System;
using System.Collections.Generic;

namespace Generator.Renderer.Public.ParameterToNativeExpressions;

internal class Bitfield : ToNativeParameterConverter
{
    public bool Supports(GirModel.AnyTypeReference anyTypeReference)
        => anyTypeReference.References<GirModel.Bitfield>();

    public void Initialize(ParameterToNativeData parameterData, IEnumerable<ParameterToNativeData> _)
    {
        if (parameterData.Parameter.Direction != GirModel.Direction.In)
            throw new NotImplementedException($"{parameterData.Parameter.AnyTypeReferenceOrVarArgs}: Bitfield with direction != in not yet supported");

        switch (parameterData.Parameter)
        {
            case { IsPointer: true }:
                Ref(parameterData);
                break;
            default:
                In(parameterData);
                break;
        }
    }

    private static void In(ParameterToNativeData parameterData)
    {
        var parameterName = Model.Parameter.GetName(parameterData.Parameter);
        parameterData.SetSignatureName(() => parameterName);
        parameterData.SetCallName(() => parameterName);
    }

    private static void Ref(ParameterToNativeData parameterData)
    {
        var parameterName = Model.Parameter.GetName(parameterData.Parameter);
        parameterData.SetSignatureName(() => parameterName);
        parameterData.SetCallName(() => ParameterDirection.Ref() + parameterName);
    }
}
