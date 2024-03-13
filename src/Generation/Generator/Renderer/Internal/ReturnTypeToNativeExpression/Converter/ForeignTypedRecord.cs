using System;

namespace Generator.Renderer.Internal.ReturnTypeToNativeExpressions;

internal class ForeignTypedRecord : ReturnTypeConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.Record>(out var record) && Model.Record.IsForeignTyped(record);

    public string GetString(GirModel.ReturnType returnType, string fromVariableName)
    {
        return returnType switch
        {
            { Transfer: GirModel.Transfer.None, Nullable: true } => $"{fromVariableName}?.Handle.DangerousGetHandle() ?? IntPtr.Zero",
            { Transfer: GirModel.Transfer.None, Nullable: false } => $"{fromVariableName}.Handle.DangerousGetHandle()",
            { Transfer: GirModel.Transfer.Full, Nullable: true } => $"{fromVariableName}?.Handle.UnownedCopy().DangerousGetHandle() ?? IntPtr.Zero",
            { Transfer: GirModel.Transfer.Full, Nullable: false } => $"{fromVariableName}.Handle.UnownedCopy().DangerousGetHandle()",
            _ => throw new Exception($"Unknown transfer type for foreigen typed record return type which should be converted to native.")
        };
    }
}
