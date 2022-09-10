using System.Collections.Generic;

namespace Generator.Renderer.Public.ParameterExpressions;

internal interface ToNativeParameterConverter
{
    bool Supports(GirModel.AnyType type);
    void Initialize(ParameterToNativeData parameterData, IEnumerable<ParameterToNativeData> parameters);
}
