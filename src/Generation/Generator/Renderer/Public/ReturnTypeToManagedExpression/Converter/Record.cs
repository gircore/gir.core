using System;
using Generator.Model;

namespace Generator.Renderer.Public.ReturnTypeToManagedExpressions;

internal class Record : ReturnTypeConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.Record>();

    public string GetString(GirModel.ReturnType returnType, string fromVariableName)
    {
        if (returnType.IsPointer)
        {
            var record = (GirModel.Record) returnType.AnyType.AsT0;
            string ctor = $"new {ComplexType.GetFullyQualified(record)}({fromVariableName})";
            if (returnType.Nullable)
                return $"{fromVariableName}.Equals(IntPtr.Zero) ? null : {ctor}";
            return ctor;
        }

        throw new NotImplementedException("Can't convert from internal records which are returnd by value to public available. This is not supported in current development branch, too.");
    }
}
