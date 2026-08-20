using System.Collections.Generic;

namespace Generator.Renderer.Public.ParameterToNativeExpressions;

internal interface ToNativeParameterConverter
{
    bool Supports(GirModel.AnyTypeReference anyTypeReference);
    void Initialize(ParameterToNativeData parameterData, IEnumerable<ParameterToNativeData> parameters);
}
