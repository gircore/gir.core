using System;
using System.Collections.Generic;
using System.Text;
using Generator.Model;

namespace Generator.Renderer.Public.ParameterToNativeExpressions;

internal class PlatformString : ToNativeParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.PlatformString>();

    public void Initialize(ParameterToNativeData parameter, IEnumerable<ParameterToNativeData> _)
    {
        // TODO - the caller needs to pass in some kind of Span<T> as a buffer that can be filled in by the C function.
        // These functions (e.g. g_unichar_to_utf8()) expect a minimum buffer size to be provided.
        if (parameter.Parameter.CallerAllocates)
            throw new NotImplementedException($"{parameter.Parameter.AnyTypeOrVarArgs}: String type with caller-allocates=1 not yet supported");

        // inout string parameters only occur for deprecated functions like pango_skip_space(), which may have incorrect ownership transfer annotations.
        // These functions just update the char** parameter to point at a different location in the provided char*, but have transfer=full and caller-allocates=0
        if (parameter.Parameter.Direction == GirModel.Direction.InOut)
            throw new NotImplementedException($"{parameter.Parameter.AnyTypeOrVarArgs}: String type with direction=inout not yet supported");

        var parameterName = Model.Parameter.GetName(parameter.Parameter);
        parameter.SetSignatureName(parameterName);

        string nativeVariableName = Model.Parameter.GetConvertedName(parameter.Parameter);

        if (parameter.Parameter.Direction == GirModel.Direction.Out)
        {
            // Note: optional parameters are generated as regular out parameters, which the caller can ignore with 'out var _' if desired.
            parameter.SetCallName($"out var {nativeVariableName}");

            // After the call, convert the resulting handle to a managed string and free the native memory right away.
            var expression = new StringBuilder();
            expression.AppendLine($"{parameterName} = {nativeVariableName}.ConvertToString();");
            expression.Append($"{nativeVariableName}.Dispose();");
            parameter.SetPostCallExpression(expression.ToString());
        }
        else
        {
            var ownedHandleTypeName = parameter.Parameter switch
            {
                { Nullable: true, Transfer: GirModel.Transfer.Full } => Model.PlatformString.GetInternalNullableUnownedHandleName(),
                { Nullable: true, Transfer: GirModel.Transfer.None } => Model.PlatformString.GetInternalNullableOwnedHandleName(),
                { Nullable: false, Transfer: GirModel.Transfer.Full } => Model.PlatformString.GetInternalNonNullableUnownedHandleName(),
                { Nullable: false, Transfer: GirModel.Transfer.None } => Model.PlatformString.GetInternalNonNullableOwnedHandleName(),
                _ => throw new Exception($"Parameter {parameter.Parameter.Name} of type {parameter.Parameter.AnyTypeOrVarArgs} not supported")
            };

            parameter.SetExpression($"using var {nativeVariableName} = {ownedHandleTypeName}.Create({parameterName});");
            parameter.SetCallName(nativeVariableName);
        }
    }
}
