using System;
using System.Collections.Generic;

namespace Generator.Renderer.Public.ParameterToNativeExpressions;

internal class GLibPointerArray : ToNativeParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.IsGLibPtrArray();

    public void Initialize(ParameterToNativeData parameter, IEnumerable<ParameterToNativeData> _)
    {
        if (parameter.Parameter.Direction != GirModel.Direction.In)
            throw new NotImplementedException($"{parameter.Parameter.AnyTypeOrVarArgs}: ptrarray parameter '{parameter.Parameter.Name}' with direction != in not yet supported");
        if (!parameter.Parameter.IsPointer)
            throw new NotImplementedException($"{parameter.Parameter.AnyTypeOrVarArgs}: non pointer ptrarray parameter '{parameter.Parameter.Name}' not yet supported");

        var parameterName = Model.Parameter.GetName(parameter.Parameter);
        parameter.SetSignatureName(() => parameterName);
        parameter.SetCallName(() => $"{parameterName}.Handle.DangerousGetHandle()");
    }
}
