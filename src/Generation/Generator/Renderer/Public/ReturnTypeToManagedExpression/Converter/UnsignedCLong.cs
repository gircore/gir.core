using System.Collections.Generic;
using GirModel;

namespace Generator.Renderer.Public.ReturnTypeToManagedExpressions;

internal class UnsignedCLong : ReturnTypeConverter
{
    public bool Supports(AnyType type)
        => type.Is<GirModel.UnsignedCLong>();

    public void Initialize(ReturnTypeToManagedData data, IEnumerable<ParameterToNativeData> _)
    {
        data.SetExpression(fromVariableName => data.ReturnType.IsPointer
            ? fromVariableName
            : $"{fromVariableName}.Value");
    }
}
