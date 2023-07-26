using System;
using GirModel;

namespace Generator.Renderer.Public.ReturnTypeToManagedExpressions;

internal class Record : ReturnTypeConverter
{
    public bool Supports(AnyType type)
        => type.Is<GirModel.Record>(out var record) && !Model.Record.IsOpaqueTyped(record);

    public string GetString(GirModel.ReturnType returnType, string fromVariableName)
    {
        if (returnType.IsPointer)
            return $"new {ReturnTypeRenderer.Render(returnType)}({fromVariableName})";

        throw new NotImplementedException("Can't convert from internal records which are returnd by value to public available. This is not supported in current development branch, too.");
    }
}
