using System.Collections.Generic;
using Generator.Model;

namespace Generator.Renderer.Public.ParameterExpressions.ToNative;

internal class InterfaceArray : ToNativeParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.IsArray<GirModel.Interface>();

    public void Initialize(ParameterToNativeData parameter, IEnumerable<ParameterToNativeData> _)
    {
        var parameterName = Parameter.GetName(parameter.Parameter);
        var nativevariableName = parameterName + "Native";

        parameter.SetSignatureName(parameterName);
        parameter.SetCallName(nativevariableName);
        parameter.SetExpression($"var {nativevariableName} = {parameterName}.Select(iface => (iface as GObject.Object).Handle).ToArray();");
    }
}
