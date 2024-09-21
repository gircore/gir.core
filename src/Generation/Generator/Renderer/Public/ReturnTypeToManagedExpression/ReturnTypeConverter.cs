using System.Collections.Generic;

namespace Generator.Renderer.Public.ReturnTypeToManagedExpressions;

internal interface ReturnTypeConverter
{
    bool Supports(GirModel.AnyType type);
    void Initialize(ReturnTypeToManagedData data, IEnumerable<ParameterToNativeData> parameters);
}
