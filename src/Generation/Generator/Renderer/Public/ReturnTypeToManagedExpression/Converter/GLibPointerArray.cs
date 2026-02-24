using System;
using System.Collections.Generic;
using GirModel;

namespace Generator.Renderer.Public.ReturnTypeToManagedExpressions;

internal class GLibPointerArray : ReturnTypeConverter
{
    public bool Supports(AnyType type)
    {
        return type.IsGLibPtrArray();
    }

    public void Initialize(ReturnTypeToManagedData data, IEnumerable<ParameterToNativeData> _)
    {
        data.SetExpression(fromVariableName =>
        {
            var returnType = data.ReturnType;

            var handleExpression = returnType switch
            {
                { Transfer: Transfer.Full } => fromVariableName,
                { Transfer: Transfer.None } => $"{fromVariableName}.OwnedCopy()",
                _ => throw new NotImplementedException("Unknown transfer type")
            };

            var createNewInstance = $"new {Model.PointerArrayType.GetFullyQualifiedPublicClassName()}({handleExpression})";

            return returnType.Nullable
                 ? $"{fromVariableName}.IsInvalid ? null : {createNewInstance}"
                 : createNewInstance;
        });
    }
}
