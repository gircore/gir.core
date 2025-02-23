using System.Collections.Generic;
using GirModel;

namespace Generator.Renderer.Public.ReturnTypeToManagedExpressions;

internal class Void : ReturnTypeConverter
{
    public bool Supports(AnyType type)
        => type.Is<GirModel.Void>();

    public void Initialize(ReturnTypeToManagedData data, IEnumerable<ParameterToNativeData> _)
    {
        //Do nothing in case of void. There is no expression to create
    }
}
