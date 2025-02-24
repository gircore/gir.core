using System.Collections.Generic;
using GirModel;

namespace Generator.Renderer.Public.ReturnTypeToManagedExpressions;

internal class Pointer : ReturnTypeConverter
{
    public bool Supports(AnyType type)
        => type.Is<GirModel.Pointer>();

    public void Initialize(ReturnTypeToManagedData data, IEnumerable<ParameterToNativeData> _)
        => data.SetExpression(fromVariableName => fromVariableName);
}
