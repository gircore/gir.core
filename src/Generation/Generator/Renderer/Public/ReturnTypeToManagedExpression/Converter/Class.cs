using System;
using Generator.Model;

namespace Generator.Renderer.Public.ReturnTypeToManagedExpressions;

internal class Class : ReturnTypeConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.Class>();

    public string GetString(GirModel.ReturnType returnType, string fromVariableName)
    {
        var cls = (GirModel.Class) returnType.AnyType.AsT0;

        if (!returnType.IsPointer)
            throw new NotImplementedException($"Can't convert {returnType} to managed as it is a pointer");

        if (cls.Fundamental)
        {
            string ctor = $"new {ComplexType.GetFullyQualified(cls)}({fromVariableName})";
            if (returnType.Nullable)
                return $"{fromVariableName}.Equals(IntPtr.Zero) ? null : {ctor}";
            return ctor;
        }

        return returnType.Nullable
            ? $"GObject.Internal.ObjectWrapper.WrapNullableHandle<{ComplexType.GetFullyQualified(cls)}>({fromVariableName}, {Transfer.IsOwnedRef(returnType.Transfer).ToString().ToLower()})"
            : $"GObject.Internal.ObjectWrapper.WrapHandle<{ComplexType.GetFullyQualified(cls)}>({fromVariableName}, {Transfer.IsOwnedRef(returnType.Transfer).ToString().ToLower()})";
    }
}
