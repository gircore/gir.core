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

        public static readonly Type Invalid = new Type((nuint) Native.BasicType.Invalid);
        public static readonly Type None = new Type((nuint) Native.BasicType.None);
        public static readonly Type Interface = new Type((nuint) Native.BasicType.Interface);
        public static readonly Type Char = new Type((nuint) Native.BasicType.Char);
        public static readonly Type UChar = new Type((nuint) Native.BasicType.UChar);
        public static readonly Type Boolean = new Type((nuint) Native.BasicType.Boolean);
        public static readonly Type Int = new Type((nuint) Native.BasicType.Int);
        public static readonly Type UInt = new Type((nuint) Native.BasicType.UInt);
        public static readonly Type Long = new Type((nuint) Native.BasicType.Long);
        public static readonly Type ULong = new Type((nuint) Native.BasicType.ULong);
        public static readonly Type Int64 = new Type((nuint) Native.BasicType.Int64);
        public static readonly Type UInt64 = new Type((nuint) Native.BasicType.UInt64);
        public static readonly Type Enum = new Type((nuint) Native.BasicType.Enum);
        public static readonly Type Flags = new Type((nuint) Native.BasicType.Flags);
        public static readonly Type Float = new Type((nuint) Native.BasicType.Float);
        public static readonly Type Double = new Type((nuint) Native.BasicType.Double);
        public static readonly Type String = new Type((nuint) Native.BasicType.String);
        public static readonly Type Pointer = new Type((nuint) Native.BasicType.Pointer);
        public static readonly Type Boxed = new Type((nuint) Native.BasicType.Boxed);
        public static readonly Type Param = new Type((nuint) Native.BasicType.Param);
        public static readonly Type Object = new Type((nuint) Native.BasicType.Object);
        public static readonly Type Variant = new Type((nuint) Native.BasicType.Variant);

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
}
