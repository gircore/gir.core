using System;
using System.Collections.Generic;

namespace Generator.Renderer.Internal.ParameterToManagedExpressions;

internal class PointerAlias : ToManagedParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.IsAlias<GirModel.Pointer>();

    public void Initialize(ParameterToManagedData parameterData, IEnumerable<ParameterToManagedData> parameters)
    {
        var variableName = Model.Parameter.GetName(parameterData.Parameter);

        parameterData.SetSignatureName(() => variableName);
        parameterData.SetCallName(() => variableName);
    }
}
