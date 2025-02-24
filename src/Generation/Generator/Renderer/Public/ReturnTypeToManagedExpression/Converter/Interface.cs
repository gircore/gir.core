using System;
using System.Collections.Generic;
using Generator.Model;

namespace Generator.Renderer.Public.ReturnTypeToManagedExpressions;

internal class Interface : ReturnTypeConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.Interface>();

    public void Initialize(ReturnTypeToManagedData data, IEnumerable<ParameterToNativeData> _)
    {
        data.SetExpression(fromVariableName =>
        {
            var returnType = data.ReturnType;

            if (!returnType.IsPointer)
                throw new NotImplementedException($"Can't convert {returnType} to managed as it is a pointer");

            var @interface = (GirModel.Interface) returnType.AnyType.AsT0;

            return returnType.Nullable
                ? $"({Model.Type.GetPublicNameFullyQuallified(@interface)}?) GObject.Internal.InstanceWrapper.WrapNullableHandle<{Model.Interface.GetFullyQualifiedImplementationName(@interface)}>({fromVariableName}, {Transfer.IsOwnedRef(returnType.Transfer).ToString().ToLower()})"
                : $"({Model.Type.GetPublicNameFullyQuallified(@interface)}) GObject.Internal.InstanceWrapper.WrapHandle<{Model.Interface.GetFullyQualifiedImplementationName(@interface)}>({fromVariableName}, {Transfer.IsOwnedRef(returnType.Transfer).ToString().ToLower()})";
        });
    }
}
