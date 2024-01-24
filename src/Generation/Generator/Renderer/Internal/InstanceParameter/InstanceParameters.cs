using System;
using System.Collections.Generic;

namespace Generator.Renderer.Internal;

internal static class InstanceParameters
{
    private static readonly List<InstanceParameter.InstanceParameterConverter> converters = new()
    {
        new InstanceParameter.Class(),
        new InstanceParameter.ForeignTypedRecord(),
        new InstanceParameter.Interface(),
        new InstanceParameter.OpaqueTypedRecord(),
        new InstanceParameter.OpaqueUntypedRecord(),
        new InstanceParameter.Pointer(),
        new InstanceParameter.Record(),
        new InstanceParameter.TypedRecord(),
        new InstanceParameter.Union()
    };

    public static string Render(GirModel.InstanceParameter instanceParameter)
    {
        foreach (var converter in converters)
            if (converter.Supports(instanceParameter.Type))
                return Render(converter.Convert(instanceParameter));

        throw new Exception($"Instance parameter \"{instanceParameter.Name}\" of type {instanceParameter.Type} can not be rendered");
    }

    private static string Render(InstanceParameter.RenderableInstanceParameter parameter)
        => $@"{parameter.NullableTypeName} {parameter.Name}";
}
