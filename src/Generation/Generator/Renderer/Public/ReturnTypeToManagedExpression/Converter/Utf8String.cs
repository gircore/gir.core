using System.Collections.Generic;
using GirModel;

namespace Generator.Renderer.Public.ReturnTypeToManagedExpressions;

internal class Utf8String : ReturnTypeConverter
{
    public bool Supports(AnyTypeReference anyTypeReference)
        => anyTypeReference.References<GirModel.Utf8String>();

    public void Initialize(ReturnTypeToManagedData data, IEnumerable<ParameterToNativeData> _)
    {
        // Convert the Utf8StringHandle return type to a string.
        data.SetExpression(fromVariableName => $"{fromVariableName}.ConvertToString()");
    }
}
