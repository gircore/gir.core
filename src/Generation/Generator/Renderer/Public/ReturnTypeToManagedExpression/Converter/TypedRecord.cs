using System;
using GirModel;

namespace Generator.Renderer.Public.ReturnTypeToManagedExpressions;

internal class TypedRecord : ReturnTypeConverter
{
    public bool Supports(AnyType type)
        => type.Is<GirModel.Record>(out var record) && Model.Record.IsTyped(record);

    public string GetString(GirModel.ReturnType returnType, string fromVariableName)
    {
        var record = (GirModel.Record) returnType.AnyType.AsT0;

        var handleExpression = returnType switch
        {
            { Transfer: Transfer.Full } => fromVariableName,
            { Transfer: Transfer.None } => $"{fromVariableName}.OwnedCopy()",
            _ => throw new NotImplementedException("Unknown transfer type")
        };

        var createNewInstance = $"new {Model.ComplexType.GetFullyQualified(record)}({handleExpression})";

        return returnType.Nullable
            ? $"{fromVariableName}.IsInvalid ? null : {createNewInstance};"
            : createNewInstance;
    }
}
