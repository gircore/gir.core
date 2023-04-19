using System;
using System.Collections.Generic;

namespace Generator.Renderer.Internal.ParameterToManagedExpressions;

internal class Pointer : ToManagedParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.Pointer>();

    public void Initialize(ParameterToManagedData parameterData, IEnumerable<ParameterToManagedData> parameters)
    {
        if (parameterData.Parameter.Direction != GirModel.Direction.In)
            throw new NotImplementedException($"{parameterData.Parameter.AnyTypeOrVarArgs}: Pointer with direction != in not yet supported");

        var variableName = Model.Parameter.GetName(parameterData.Parameter);

        parameterData.SetSignatureName(variableName);
        parameterData.SetCallName(variableName);

        if (parameterData.Parameter.Closure is not null)
            parameterData.IsCallbackUserData = true;
    }
}
