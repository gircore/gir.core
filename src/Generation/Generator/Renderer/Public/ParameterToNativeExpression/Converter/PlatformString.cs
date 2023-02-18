using System;
using System.Collections.Generic;
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
            throw new NotImplementedException($"{parameter.Parameter.AnyType}: String type with caller-allocates=1 not yet supported");

        // TODO - the default marshalling for 'ref string' produces crashes for functions
        // like pango_skip_space(), so custom marshalling may be required.
        if (parameter.Parameter.Direction == GirModel.Direction.InOut)
            throw new NotImplementedException($"{parameter.Parameter.AnyType}: String type with direction=inout not yet supported");

        // TODO - support output strings
        if (parameter.Parameter.Direction == GirModel.Direction.Out)
            throw new NotImplementedException($"{parameter.Parameter.AnyType}: String type with direction=out not yet supported");

        var parameterName = Parameter.GetName(parameter.Parameter);
        parameter.SetSignatureName(parameterName);

        string nativeVariableName = Parameter.GetConvertedName(parameter.Parameter);

        string ownedHandleTypeName = parameter.Parameter.Nullable
            ? Model.PlatformString.GetInternalNullableOwnedHandleName()
            : Model.PlatformString.GetInternalNonNullableOwnedHandleName();

        parameter.SetExpression($"var {nativeVariableName} = {ownedHandleTypeName}.Create({parameterName});");
        parameter.SetCallName(nativeVariableName);
    }
}
