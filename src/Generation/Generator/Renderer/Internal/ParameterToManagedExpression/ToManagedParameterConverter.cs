using System.Collections.Generic;

namespace Generator.Renderer.Internal.ParameterToManagedExpressions;

internal interface ToManagedParameterConverter
{
    bool Supports(GirModel.AnyTypeReference anyTypeReference);
    void Initialize(ParameterToManagedData parameterData, IEnumerable<ParameterToManagedData> parameters);
}
