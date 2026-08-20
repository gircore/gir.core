using System.Collections.Generic;

namespace Generator.Renderer.Public.ReturnTypeToManagedExpressions;

internal interface ReturnTypeConverter
{
    bool Supports(GirModel.AnyTypeReference anyTypeReference);
    void Initialize(ReturnTypeToManagedData data, IEnumerable<ParameterToNativeData> parameters);
}
