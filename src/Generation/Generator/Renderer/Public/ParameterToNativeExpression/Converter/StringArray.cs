using System;
using System.Collections.Generic;
using Generator.Model;

namespace Generator.Renderer.Public.ParameterToNativeExpressions;

internal class StringArray : ToNativeParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.IsArray<GirModel.String>();

    public void Initialize(ParameterToNativeData parameter, IEnumerable<ParameterToNativeData> _)
    {
        if (parameter.Parameter.Direction != GirModel.Direction.In)
            throw new NotImplementedException($"{parameter.Parameter.AnyType}: String array type with direction != in not yet supported");

        var arrayType = parameter.Parameter.AnyType.AsT1;
        if (parameter.Parameter.Transfer == GirModel.Transfer.None && arrayType.Length == null)
        {
            var variableName = Parameter.GetName(parameter.Parameter);
            var nativeVariableName = variableName + "Native";

            parameter.SetSignatureName(variableName);
            parameter.SetCallName(nativeVariableName);
            parameter.SetExpression($"var {nativeVariableName} = new GLib.Internal.StringArrayNullTerminatedSafeHandle({variableName}).DangerousGetHandle();");
        }
        else
        {
            //We don't need any conversion for string[]
            var variableName = Parameter.GetName(parameter.Parameter);
            parameter.SetSignatureName(variableName);
            parameter.SetCallName(variableName);
        }
    }
}
