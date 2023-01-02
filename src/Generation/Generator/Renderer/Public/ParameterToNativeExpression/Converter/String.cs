using System;
using System.Collections.Generic;
using Generator.Model;

namespace Generator.Renderer.Public.ParameterToNativeExpressions;

internal class String : ToNativeParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.String>();

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

        var prefix = parameter.Parameter.Direction == GirModel.Direction.Out
            ? "out "
            : string.Empty;

        var parameterName = Parameter.GetName(parameter.Parameter);
        parameter.SetSignatureName(parameterName);
        parameter.SetCallName(prefix + parameterName);
    }
}
