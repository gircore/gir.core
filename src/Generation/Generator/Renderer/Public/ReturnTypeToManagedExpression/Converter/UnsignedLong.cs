using System.Collections.Generic;
using GirModel;

namespace Generator.Renderer.Public.ReturnTypeToManagedExpressions;

internal class UnsignedLong : ReturnTypeConverter
{
    public bool Supports(AnyType type)
        => type.Is<GirModel.UnsignedLong>();

    public void Initialize(ReturnTypeToManagedData data, IEnumerable<ParameterToNativeData> _)
        => data.SetExpression(fromVariableName => fromVariableName); //Valid for IsPointer = true && IsPointer = false
}
