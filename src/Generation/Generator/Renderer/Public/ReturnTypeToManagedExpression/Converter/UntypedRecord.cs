using System;
using System.Collections.Generic;
using GirModel;

namespace Generator.Renderer.Public.ReturnTypeToManagedExpressions;

internal class UntypedRecord : ReturnTypeConverter
{
    public bool Supports(AnyType type)
        => type.Is<GirModel.Record>(out var record) && Model.Record.IsUntyped(record);

    public void Initialize(ReturnTypeToManagedData data, IEnumerable<ParameterToNativeData> _)
    {
        data.SetExpression(fromVariableName =>
        {
            var returnType = data.ReturnType;
            var record = (GirModel.Record) returnType.AnyType.AsT0;

            var handleExpression = returnType switch
            {
                { Transfer: Transfer.Full } => fromVariableName,
                { Transfer: Transfer.None } => $"{fromVariableName}.Copy()",
                { Transfer: Transfer.Container } => fromVariableName,
                _ => throw new NotImplementedException($"Unsupported transfer type '{returnType.Transfer}' for untyped record {record.Name}")
            };

            var createNewInstance = $"new {Model.ComplexType.GetFullyQualified(record)}({handleExpression})";

            return returnType.Nullable
                ? $"{fromVariableName}.IsInvalid ? null : {createNewInstance}"
                : createNewInstance;
        });
    }
}
