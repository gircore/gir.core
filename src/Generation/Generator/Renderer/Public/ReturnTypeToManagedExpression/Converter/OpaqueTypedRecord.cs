using System;
using System.Collections.Generic;

namespace Generator.Renderer.Public.ReturnTypeToManagedExpressions;

internal class OpaqueTypedRecord : ReturnTypeConverter
{
    public bool Supports(GirModel.AnyTypeReference anyTypeReference)
        => anyTypeReference.References<GirModel.Record>(out var record) && Model.Record.IsOpaqueTyped(record);

    public void Initialize(ReturnTypeToManagedData data, IEnumerable<ParameterToNativeData> _)
    {
        data.SetExpression(fromVariableName =>
        {
            var returnType = data.ReturnType;

            var record = (GirModel.Record) returnType.AnyTypeReference.AsT0.Type;

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
