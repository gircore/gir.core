using System;

namespace Generator3.Converter
{
    internal static class TypeNameExtension
    {
        public const string Pointer = "IntPtr";
        public const string PointerArray = "IntPtr[]";

        public static string GetName(this GirModel.Type type)
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
                GirModel.NativeUnsignedInteger => "nuint",
                GirModel.Pointer => Pointer,
                GirModel.UnsignedPointer => "UIntPtr",

                _ => throw new Exception($"Can't convert type {type} into a code representation.")
            };
        }
    }
}
