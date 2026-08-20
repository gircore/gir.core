using System;
using System.Collections.Generic;

namespace Generator.Renderer.Internal.ParameterToManagedExpressions;

internal class PointerAlias : ToManagedParameterConverter
{
    public bool Supports(GirModel.AnyTypeReference anyTypeReference)
        => anyTypeReference.ReferencesAlias<GirModel.Pointer>();

    public void Initialize(ParameterToManagedData parameterData, IEnumerable<ParameterToManagedData> parameters)
    {
        var variableName = Model.Parameter.GetName(parameterData.Parameter);

        parameterData.SetSignatureName(() => variableName);
        parameterData.SetCallName(() => variableName);
    }
}
