using System;
using Generator3.Model.Public;
using GirModel;

namespace Generator3.Converter.ReturnType.ToManaged;

internal class Record : ReturnTypeConverter
{
    public bool Supports(AnyType type)
        => type.Is<GirModel.Record>();

    public string GetString(GirModel.ReturnType returnType, string fromVariableName)
    {
        var to = returnType.CreatePublicModel();

        if (returnType.IsPointer)
            return $"new {to.NullableTypeName}({fromVariableName})";

        throw new NotImplementedException("Can't convert from internal records which are returnd by value to public available. This is not supported in current development branch, too.");
    }
}
