using System;
using System.Collections.Generic;
using Generator.Model;

namespace Generator.Renderer.Public.ParameterExpressions.ToNative;

internal class Pointer : ToNativeParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.Pointer>();

    public void Initialize(ParameterToNativeData parameter, IEnumerable<ParameterToNativeData> _)
    {
        if (parameter.IsClosure)
        {
            //User data is not used
            parameter.SetSignatureName(Parameter.GetName(parameter.Parameter));
            parameter.SetCallName("IntPtr.Zero");
        }
        else
        {
            throw new Exception("Pointer parameter not yet supported.");
        }
    }
}
