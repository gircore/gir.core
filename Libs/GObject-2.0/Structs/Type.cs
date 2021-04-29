using System;
using System.Runtime.InteropServices;
using GLib;
using GLib.Native;

namespace GObject
{
    [StructLayout(LayoutKind.Explicit, Size = 8)]
    public struct Type
    {
        #region Fields

        [FieldOffset(0)] private readonly nuint _value;

        public nuint Value => _value;

        #endregion

        #region Statics

        public static readonly Type Invalid = new Type((nuint) BasicTypes.Invalid);
        public static readonly Type None = new Type((nuint) BasicTypes.None);
        public static readonly Type Interface = new Type((nuint) BasicTypes.Interface);
        public static readonly Type Char = new Type((nuint) BasicTypes.Char);
        public static readonly Type UChar = new Type((nuint) BasicTypes.UChar);
        public static readonly Type Boolean = new Type((nuint) BasicTypes.Boolean);
        public static readonly Type Int = new Type((nuint) BasicTypes.Int);
        public static readonly Type UInt = new Type((nuint) BasicTypes.UInt);
        public static readonly Type Long = new Type((nuint) BasicTypes.Long);
        public static readonly Type ULong = new Type((nuint) BasicTypes.ULong);
        public static readonly Type Int64 = new Type((nuint) BasicTypes.Int64);
        public static readonly Type UInt64 = new Type((nuint) BasicTypes.UInt64);
        public static readonly Type Enum = new Type((nuint) BasicTypes.Enum);
        public static readonly Type Flags = new Type((nuint) BasicTypes.Flags);
        public static readonly Type Float = new Type((nuint) BasicTypes.Float);
        public static readonly Type Double = new Type((nuint) BasicTypes.Double);
        public static readonly Type String = new Type((nuint) BasicTypes.String);
        public static readonly Type Pointer = new Type((nuint) BasicTypes.Pointer);
        public static readonly Type Boxed = new Type((nuint) BasicTypes.Boxed);
        public static readonly Type Param = new Type((nuint) BasicTypes.Param);
        public static readonly Type Object = new Type((nuint) BasicTypes.Object);
        public static readonly Type Variant = new Type((nuint) BasicTypes.Variant);

        #endregion Statics

        #region Constructors

        public Type(nuint value)
        {
            _value = value;
        }

        #endregion

        // Print out the name of the GType
        public override string ToString()
        {
            return StringHelper.ToStringUtf8(Native.Functions.TypeName(_value));
        }

        //Offsets see: https://gitlab.gnome.org/GNOME/glib/blob/master/gobject/gtype.h
    }

    internal enum BasicTypes
    {
        Invalid = 0 << 2,
        None = 1 << 2,
        Interface = 2 << 2,
        Char = 3 << 2,
        UChar = 4 << 2,
        Boolean = 5 << 2,
        Int = 6 << 2,
        UInt = 7 << 2,
        Long = 8 << 2,
        ULong = 9 << 2,
        Int64 = 10 << 2,
        UInt64 = 11 << 2,
        Enum = 12 << 2,
        Flags = 13 << 2,
        Float = 14 << 2,
        Double = 15 << 2,
        String = 16 << 2,
        Pointer = 17 << 2,
        Boxed = 18 << 2,
        Param = 19 << 2,
        Object = 20 << 2,
        Variant = 21 << 2
    }
}
