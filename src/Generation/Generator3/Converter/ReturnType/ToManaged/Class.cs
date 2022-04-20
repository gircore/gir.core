using System;
using Generator3.Model.Public;
using GirModel;

namespace Generator3.Converter.ReturnType.ToManaged;

internal class Class : ReturnTypeConverter
{
    public bool Supports(AnyType type)
        => type.Is<GirModel.Class>();

    public string GetString(GirModel.ReturnType returnType, string fromVariableName)
    {
        var cls = (GirModel.Class) returnType.AnyType.AsT0;

        if (!returnType.IsPointer)
            throw new NotImplementedException($"Can't convert {returnType} to managed as it is a pointer");

        var to = returnType.CreatePublicModel();

        return cls.IsFundamental
            ? $"new {cls.GetFullyQualified()}({fromVariableName})"
            : $"GObject.Internal.ObjectWrapper.WrapHandle<{to.NullableTypeName}>({fromVariableName}, {returnType.Transfer.IsOwnedRef().ToString().ToLower()})";
    }
}
