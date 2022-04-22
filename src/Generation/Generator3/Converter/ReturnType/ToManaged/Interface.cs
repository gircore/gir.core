using System;
using Generator3.Model.Public;
using GirModel;

namespace Generator3.Converter.ReturnType.ToManaged;

internal class Interface : ReturnTypeConverter
{
    public bool Supports(AnyType type)
        => type.Is<GirModel.Interface>();

    public string GetString(GirModel.ReturnType returnType, string fromVariableName)
    {
        if (!returnType.IsPointer)
            throw new NotImplementedException($"Can't convert {returnType} to managed as it is a pointer");

        var to = returnType.CreatePublicModel();

        return $"GObject.Internal.ObjectWrapper.WrapHandle<{to.NullableTypeName}>({fromVariableName}, {returnType.Transfer.IsOwnedRef().ToString().ToLower()})";
    }
}
