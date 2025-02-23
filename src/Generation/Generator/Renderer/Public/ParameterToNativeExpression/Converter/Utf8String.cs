using System;
using System.Collections.Generic;
using System.Text;

namespace Generator.Renderer.Public.ParameterToNativeExpressions;

internal class Utf8String : ToNativeParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.Utf8String>();

    public void Initialize(ParameterToNativeData parameter, IEnumerable<ParameterToNativeData> _)
    {
        // TODO - the caller needs to pass in some kind of Span<T> as a buffer that can be filled in by the C function.
        // These functions (e.g. g_unichar_to_utf8()) expect a minimum buffer size to be provided.
        if (parameter.Parameter.CallerAllocates)
            throw new NotImplementedException($"{parameter.Parameter.AnyTypeOrVarArgs}: String type with caller-allocates=1 not yet supported");

        switch (parameter.Parameter.Direction)
        {
            case GirModel.Direction.In:
                In(parameter);
                break;
            case GirModel.Direction.Out:
                Out(parameter);
                break;
            case GirModel.Direction.InOut:
                // inout string parameters only occur for deprecated functions like pango_skip_space(), which may have incorrect ownership transfer annotations.
                // These functions just update the char** parameter to point at a different location in the provided char*, but have transfer=full and caller-allocates=0
                throw new NotImplementedException($"{parameter.Parameter.AnyTypeOrVarArgs}: String type with direction=inout not yet supported");
            default:
                throw new NotImplementedException("Unknown direction");
        }
    }

    private static void In(ParameterToNativeData parameter)
    {
        var nativeVariableName = Model.Parameter.GetConvertedName(parameter.Parameter);
        var parameterName = Model.Parameter.GetName(parameter.Parameter);
        parameter.SetSignatureName(() => parameterName);

        var ownedHandleTypeName = parameter.Parameter switch
        {
            { Nullable: true, Transfer: GirModel.Transfer.Full } => Model.Utf8String.GetInternalNullableUnownedHandleName(),
            { Nullable: true, Transfer: GirModel.Transfer.None } => Model.Utf8String.GetInternalNullableOwnedHandleName(),
            { Nullable: false, Transfer: GirModel.Transfer.Full } => Model.Utf8String.GetInternalNonNullableUnownedHandleName(),
            { Nullable: false, Transfer: GirModel.Transfer.None } => Model.Utf8String.GetInternalNonNullableOwnedHandleName(),
            _ => throw new Exception($"Parameter {parameter.Parameter.Name} of type {parameter.Parameter.AnyTypeOrVarArgs} not supported")
        };

        parameter.SetExpression(() => $"using var {nativeVariableName} = {ownedHandleTypeName}.Create({parameterName});");
        parameter.SetCallName(() => nativeVariableName);
    }

    private static void Out(ParameterToNativeData parameter)
    {
        var nativeVariableName = Model.Parameter.GetConvertedName(parameter.Parameter);
        var parameterName = Model.Parameter.GetName(parameter.Parameter);
        parameter.SetSignatureName(() => parameterName);
        // Note: optional parameters are generated as regular out parameters, which the caller can ignore with 'out var _' if desired.
        parameter.SetCallName(() => $"out var {nativeVariableName}");

        // After the call, convert the resulting handle to a managed string and free the native memory right away.
        var expression = new StringBuilder();
        expression.AppendLine($"{parameterName} = {nativeVariableName}.ConvertToString();");
        expression.Append($"{nativeVariableName}.Dispose();");
        parameter.SetPostCallExpression(() => expression.ToString());
    }
}
