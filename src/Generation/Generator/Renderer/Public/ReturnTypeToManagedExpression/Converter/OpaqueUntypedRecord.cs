using System;
using System.Collections.Generic;

namespace Generator.Renderer.Public.ReturnTypeToManagedExpressions;

internal class OpaqueUntypedRecord : ReturnTypeConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.Record>(out var record) && Model.Record.IsOpaqueUntyped(record);

    public void Initialize(ReturnTypeToManagedData data, IEnumerable<ParameterToNativeData> _)
    {
        data.SetExpression(fromVariableName =>
        {
            var returnType = data.ReturnType;
            var record = (GirModel.Record) returnType.AnyType.AsT0;

            var handleExpression = returnType switch
            {
                { Transfer: GirModel.Transfer.Full } => fromVariableName,
                { Transfer: GirModel.Transfer.None } => $"{fromVariableName}.OwnedCopy()",
                _ => throw new NotImplementedException("Unknown transfer type")
            };

            var createNewInstance = $"new {Model.ComplexType.GetFullyQualified(record)}({handleExpression})";

            return returnType.Nullable
                ? $"{fromVariableName}.IsInvalid ? null : {createNewInstance}"
                : createNewInstance;
        });
    }
}
