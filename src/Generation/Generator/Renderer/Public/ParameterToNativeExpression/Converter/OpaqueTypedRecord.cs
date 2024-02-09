using System;
using System.Collections.Generic;

namespace Generator.Renderer.Public.ParameterToNativeExpressions;

internal class OpaqueTypedRecord : ToNativeParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.Record>(out var record) && Model.Record.IsOpaqueTyped(record);

    public void Initialize(ParameterToNativeData parameter, IEnumerable<ParameterToNativeData> _)
    {
        if (parameter.Parameter.Direction != GirModel.Direction.In)
            throw new NotImplementedException($"{parameter.Parameter.AnyTypeOrVarArgs}: opaque record parameter '{parameter.Parameter.Name}' with direction != in not yet supported");

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
            _ => throw new Exception($"Can't detect call name for parameter opaque parameter {parameter.Parameter.Name}")
        };

        parameter.SetSignatureName(() => signatureName);
        parameter.SetCallName(() => callName);
    }
}
