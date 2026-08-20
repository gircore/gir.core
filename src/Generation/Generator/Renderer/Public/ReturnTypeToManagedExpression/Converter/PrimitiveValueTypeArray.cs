using System.Collections.Generic;
using GirModel;

namespace Generator.Renderer.Public.ReturnTypeToManagedExpressions;

internal class PrimitiveValueTypeArray : ReturnTypeConverter
{
    public bool Supports(AnyTypeReference anyTypeReference)
        => anyTypeReference.ReferencesArray<GirModel.PrimitiveValueType>();

    public void Initialize(ReturnTypeToManagedData data, IEnumerable<ParameterToNativeData> _)
        => data.SetExpression(fromVariableName => fromVariableName);
}
