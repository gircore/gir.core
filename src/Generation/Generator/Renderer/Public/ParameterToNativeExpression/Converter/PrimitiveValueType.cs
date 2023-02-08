using System;
using System.Collections.Generic;
using Generator.Model;

namespace Generator.Renderer.Public.ParameterToNativeExpressions;

internal class PrimitiveValueType : ToNativeParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.PrimitiveValueType>();

    public void Initialize(ParameterToNativeData parameter, IEnumerable<ParameterToNativeData> _)
    {
        // When there is a pointer type with direction=in, the native functions are often expecting
        // the pointer to be an array, e.g. gdk_pango_layout_get_clip_region(), so in general this
        // is not safe to generate bindings for.
        if (parameter.Parameter.IsPointer && parameter.Parameter.Direction == GirModel.Direction.In)
            throw new NotImplementedException($"{parameter.Parameter.AnyType}: Pointed primitive value types with direction == in can not yet be converted to native");

        // Add the direction keyword, e.g. ref or out, when calling the native function.
        var direction = PrimitiveValueTypeParameter.GetDirection(parameter.Parameter);

        //We don't need any conversion for native parameters
        var parameterName = Parameter.GetName(parameter.Parameter);
        parameter.SetSignatureName(parameterName);
        parameter.SetCallName(direction + parameterName);
    }
}
