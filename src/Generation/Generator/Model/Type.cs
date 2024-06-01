using System;

namespace Generator.Model;

internal static class Type
{
    public const string Pointer = "IntPtr";
    public const string PointerArray = "IntPtr[]";

    public static string GetName(GirModel.Type type)
    {
        return type switch
        {
            GirModel.ComplexType c => c.Name,
            GirModel.String => "string",
            GirModel.Integer => "int",
            GirModel.Double => "double",
            GirModel.Float => "float",
            GirModel.SignedByte => "sbyte",
            GirModel.Short => "short",
            GirModel.Long => "long",
            GirModel.UnsignedShort => "ushort",
            GirModel.UnsignedInteger => "uint",
            GirModel.UnsignedLong => "ulong",
            GirModel.Byte => "byte",
            GirModel.Bool => "bool",
            GirModel.Void => "void",
            GirModel.NativeInteger => "nint",
            GirModel.NativeUnsignedInteger => "nuint",
            GirModel.Pointer => Pointer,
            GirModel.UnsignedPointer => "UIntPtr",
            GirModel.Alias { Type: GirModel.PrimitiveType p } a => a.Name,
            GirModel.Alias { Type: GirModel.ComplexType c } => c.Name,

            _ => throw new Exception($"Can't convert type {type} into a code representation.")
        };
    }

    public static string GetPublicNameFullyQuallified(GirModel.Type type)
    {
        return type switch
        {
            GirModel.Alias a => $"{Namespace.GetPublicName(a.Namespace)}.{GetName(type)}",
            GirModel.ComplexType ct => $"{Namespace.GetPublicName(ct.Namespace)}.{GetName(type)}",
            _ => GetName(type)
        };
    }
}
