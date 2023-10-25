using System;
using System.Collections.Generic;

namespace Generator.Renderer.Internal.ParameterToManagedExpressions;

internal class OpaqueUntypedRecord : ToManagedParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.Record>(out var record) && Model.Record.IsOpaqueUntyped(record);

    public void Initialize(ParameterToManagedData parameterData, IEnumerable<ParameterToManagedData> parameters)
    {
        if (parameterData.Parameter.Direction != GirModel.Direction.In)
            throw new NotImplementedException($"{parameterData.Parameter.AnyTypeOrVarArgs}: opaque untyped record with direction != in not yet supported");

        var record = (GirModel.Record) parameterData.Parameter.AnyTypeOrVarArgs.AsT0.AsT0;
        var variableName = Model.Parameter.GetConvertedName(parameterData.Parameter);

        var signatureName = Model.Parameter.GetName(parameterData.Parameter);

        var ownedHandle = parameterData.Parameter switch
        {
            { Transfer: GirModel.Transfer.Full } => $"new {Model.OpaqueUntypedRecord.GetFullyQuallifiedOwnedHandle(record)}({signatureName})",
            { Transfer: GirModel.Transfer.None } => $"{Model.OpaqueUntypedRecord.GetFullyQuallifiedOwnedHandle(record)}.FromUnowned({signatureName})",
            _ => throw new Exception($"Unknown transfer type for opaque untyped record parameter {parameterData.Parameter.Name}")
        };

        var nullable = parameterData.Parameter.Nullable
            ? $" {signatureName} == IntPtr.Zero ? null :"
            : string.Empty;

        parameterData.SetSignatureName(signatureName);
        parameterData.SetExpression($"var {variableName} ={nullable} new {Model.OpaqueUntypedRecord.GetFullyQualifiedPublicClassName(record)}({ownedHandle});");
        parameterData.SetCallName(variableName);
    }
}
