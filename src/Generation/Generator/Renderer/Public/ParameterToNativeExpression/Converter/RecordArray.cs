using System;
using System.Collections.Generic;
using System.Text;
using Generator.Model;

namespace Generator.Renderer.Public.ParameterToNativeExpressions;

internal class RecordArray : ToNativeParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.IsArray<GirModel.Record>();

    public void Initialize(ParameterToNativeData parameter, IEnumerable<ParameterToNativeData> _)
    {
        var arrayType = parameter.Parameter.AnyTypeOrVarArgs.AsT0.AsT1;

        if (!arrayType.IsPointer)
            throw new NotImplementedException($"{parameter.Parameter.AnyTypeOrVarArgs}: Not pointed array record types can not yet be converted to native.");

        if (parameter.Parameter.Direction == GirModel.Direction.Out)
            throw new NotImplementedException($"{parameter.Parameter.AnyTypeOrVarArgs}: Array record type with direction=out not yet supported.");

        var parameterName = Model.Parameter.GetName(parameter.Parameter);
        var nativeVariableName = parameterName + "Native";

        parameter.SetSignatureName(parameterName);
        parameter.SetCallName(nativeVariableName);

        var expression = new StringBuilder();
        expression.Append($"var {nativeVariableName} = {parameterName}.Select(record => record.Handle.DangerousGetHandle())");
        if (arrayType.IsZeroTerminated)
            expression.Append(".Append(IntPtr.Zero)");

        expression.Append(".ToArray();");
        parameter.SetExpression(expression.ToString());
    }
}
