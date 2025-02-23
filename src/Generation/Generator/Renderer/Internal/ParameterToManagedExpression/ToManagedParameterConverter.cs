using System.Collections.Generic;

namespace Generator.Renderer.Internal.ParameterToManagedExpressions;

internal interface ToManagedParameterConverter
{
    bool Supports(GirModel.AnyType type);
    void Initialize(ParameterToManagedData parameterData, IEnumerable<ParameterToManagedData> parameters);
}
