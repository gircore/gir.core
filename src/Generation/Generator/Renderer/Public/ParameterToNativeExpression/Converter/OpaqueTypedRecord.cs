using System;
using System.Collections.Generic;

namespace Generator.Renderer.Public.ParameterToNativeExpressions;

internal class OpaqueTypedRecord : ToNativeParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.Record>(out var record) && Model.Record.IsOpaqueTyped(record);

    public void Initialize(ParameterToNativeData parameter, IEnumerable<ParameterToNativeData> _)
    {
        switch (parameter.Parameter.Direction)
        {
            case GirModel.Direction.In:
                In(parameter);
                break;
            case GirModel.Direction.Out:
                Out(parameter);
                break;
            default:
                throw new NotImplementedException($"{parameter.Parameter.AnyTypeOrVarArgs}: opaque record parameter '{parameter.Parameter.Name}' with direction = {parameter.Parameter.Direction} not yet supported");
        }
    }

    private static void In(ParameterToNativeData parameter)
    {
        var record = (GirModel.Record) parameter.Parameter.AnyTypeOrVarArgs.AsT0.AsT0;
        var typeHandle = Model.OpaqueTypedRecord.GetFullyQuallifiedInternalHandle(record);
        var nullHandle = Model.OpaqueTypedRecord.GetFullyQuallifiedNullHandle(record);
        var signatureName = Model.Parameter.GetName(parameter.Parameter);

        var callName = parameter.Parameter switch
        {
            { Nullable: true, Transfer: GirModel.Transfer.None } => $"({typeHandle}?) {signatureName}?.Handle ?? {nullHandle}",
            { Nullable: false, Transfer: GirModel.Transfer.None } => $"{signatureName}.Handle",
            { Nullable: true, Transfer: GirModel.Transfer.Full } => $"{signatureName}?.Handle.UnownedCopy() ?? {nullHandle}",
            { Nullable: false, Transfer: GirModel.Transfer.Full } => $"{signatureName}.Handle.UnownedCopy()",
            _ => throw new Exception($"Can't detect call name for opaque parameter {parameter.Parameter.Name}: Nullable {parameter.Parameter.Nullable}, Transfer {parameter.Parameter.Transfer}")
        };

        parameter.SetSignatureName(() => signatureName);
        parameter.SetCallName(() => callName);
    }

    private static void Out(ParameterToNativeData parameter)
    {
        var record = (GirModel.Record) parameter.Parameter.AnyTypeOrVarArgs.AsT0.AsT0;
        var className = Model.OpaqueTypedRecord.GetFullyQualifiedPublicClassName(record);
        var unownedTypeHandle = Model.OpaqueTypedRecord.GetFullyQuallifiedUnownedHandle(record);
        var ownedTypeHandle = Model.OpaqueTypedRecord.GetFullyQuallifiedOwnedHandle(record);
        var signatureName = Model.Parameter.GetName(parameter.Parameter);
        var signatureNameHandle = $"{signatureName}Handle";

        var callName = parameter.Parameter switch
        {
            { Transfer: GirModel.Transfer.None } => $"out {unownedTypeHandle} {signatureNameHandle}",
            { Transfer: GirModel.Transfer.Full } => $"out {ownedTypeHandle} {signatureNameHandle}",
            _ => throw new Exception($"Can't detect call name for opaque out parameter {parameter.Parameter.Name}: Nullable {parameter.Parameter.Nullable}, Transfer {parameter.Parameter.Transfer}")
        };

        parameter.SetSignatureName(() => signatureName);
        parameter.SetCallName(() => callName);

        var copy = parameter.Parameter.Transfer switch
        {
            GirModel.Transfer.None => ".OwnedCopy()",
            GirModel.Transfer.Full => string.Empty,
            _ => throw new NotSupportedException()
        };

        parameter.SetPostCallExpression(() => parameter.Parameter.Nullable
            ? $"{signatureName} = {signatureNameHandle}.IsInvalid ? null : new {className}({signatureNameHandle}{copy});"
            : $"{signatureName} = new {className}({signatureNameHandle}{copy});");
    }
}
