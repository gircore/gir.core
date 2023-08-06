using System;
using GirModel;

namespace Generator.Renderer.Internal.ReturnTypeToNativeExpressions;

internal class Utf8String : ReturnTypeConverter
{
    public bool Supports(AnyType type)
        => type.Is<GirModel.Utf8String>();

    public string GetString(GirModel.ReturnType returnType, string fromVariableName)
    {
        return returnType switch
        {
            // Return a string that lives as long as the "ConstantString" instance lives.
            { Transfer: Transfer.None, Nullable: true } => $"{fromVariableName}?.GetHandle() ?? System.IntPtr.Zero",
            { Transfer: Transfer.None } => $"{fromVariableName}.GetHandle()",

            // Return a string that the native caller will own and we do not free.
            { Transfer: Transfer.Full } => $"GLib.Internal.StringHelper.StringToPtrUtf8({fromVariableName})",

            { Transfer: Transfer.Container } => throw new Exception("String return type with transfer=container not yet supported"),
            _ => throw new Exception($"Unknown transfer type {returnType.Transfer}")
        };
    }
}
