using System;
using System.Collections.Generic;
using Generator.Model;

namespace Generator.Renderer.Public.ReturnTypeToManagedExpressions;

internal class Class : ReturnTypeConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.Class>();

    public void Initialize(ReturnTypeToManagedData data, IEnumerable<ParameterToNativeData> _)
    {
        data.SetExpression(fromVariableName =>
        {
            var returnType = data.ReturnType;

            if (!returnType.IsPointer)
                throw new NotImplementedException($"Can't convert {returnType} to managed as it is a pointer");

            var cls = (GirModel.Class) returnType.AnyType.AsT0;
            return cls.Fundamental
                ? Fundamental(cls, returnType, fromVariableName)
                : Standard(cls, returnType, fromVariableName);
        });
    }

    private static string Fundamental(GirModel.Class cls, GirModel.ReturnType returnType, string fromVariableName)
    {
        var ctor = $"new {ComplexType.GetFullyQualified(cls)}({fromVariableName})";

        return returnType.Nullable
            ? $"{fromVariableName} == IntPtr.Zero ? null : {ctor}"
            : ctor;
    }

    private static string Standard(GirModel.Class cls, GirModel.ReturnType returnType, string fromVariableName)
    {
        var type = ComplexType.GetFullyQualified(cls);
        return returnType.Nullable
            ? $"({type}?) GObject.Internal.InstanceWrapper.WrapNullableHandle<{type}>({fromVariableName}, {Transfer.IsOwnedRef(returnType.Transfer).ToString().ToLower()})"
            : $"({type}) GObject.Internal.InstanceWrapper.WrapHandle<{type}>({fromVariableName}, {Transfer.IsOwnedRef(returnType.Transfer).ToString().ToLower()})";
    }
}
