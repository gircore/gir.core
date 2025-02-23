using System.Collections.Generic;
using GirModel;

namespace Generator.Renderer.Public.ReturnTypeToManagedExpressions;

internal class PrimitiveValueTypeArray : ReturnTypeConverter
{
    public bool Supports(AnyType type)
        => type.IsArray<GirModel.PrimitiveValueType>();

    public void Initialize(ReturnTypeToManagedData data, IEnumerable<ParameterToNativeData> _)
        => data.SetExpression(fromVariableName => fromVariableName);
}
