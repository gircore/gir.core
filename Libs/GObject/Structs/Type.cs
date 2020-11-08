using System;
using System.Runtime.InteropServices;

namespace GObject
{
    [StructLayout(LayoutKind.Explicit, Size = 8)]
    public struct Type
    {
        #region Fields

        [FieldOffset(0)] private readonly ulong _value;

        public ulong Value => _value;

        #endregion

        #region Statics

        public static readonly Type Invalid = new Type((ulong) Types.Invalid);
        public static readonly Type None = new Type((ulong) Types.None);
        public static readonly Type Interface = new Type((ulong) Types.Interface);
        public static readonly Type Char = new Type((ulong) Types.Char);
        public static readonly Type UChar = new Type((ulong) Types.UChar);
        public static readonly Type Boolean = new Type((ulong) Types.Boolean);
        public static readonly Type Int = new Type((ulong) Types.Int);
        public static readonly Type UInt = new Type((ulong) Types.UInt);
        public static readonly Type Long = new Type((ulong) Types.Long);
        public static readonly Type ULong = new Type((ulong) Types.ULong);
        public static readonly Type Int64 = new Type((ulong) Types.Int64);
        public static readonly Type UInt64 = new Type((ulong) Types.UInt64);
        public static readonly Type Enum = new Type((ulong) Types.Enum);
        public static readonly Type Flags = new Type((ulong) Types.Flags);
        public static readonly Type Float = new Type((ulong) Types.Float);
        public static readonly Type Double = new Type((ulong) Types.Double);
        public static readonly Type String = new Type((ulong) Types.String);
        public static readonly Type Pointer = new Type((ulong) Types.Pointer);
        public static readonly Type Boxed = new Type((ulong) Types.Boxed);
        public static readonly Type Param = new Type((ulong) Types.Param);
        public static readonly Type Object = new Type((ulong) Types.Object);
        public static readonly Type Variant = new Type((ulong) Types.Variant);

        #endregion Statics

        #region Constructors

        public Type(ulong value)
        {
            _value = value;
        }

        #endregion

        #region Methods
        
        public static bool IsA(Type gtype, Type type)
            => IsA(gtype.Value, type.Value);

        internal static bool IsA(Type gtype, Types type)
            => IsA(gtype.Value, type);
        
        internal static bool IsA(ulong gtype, Types type)
            => IsA(gtype, (ulong) type);

        internal static bool IsA(ulong gtype, ulong type)
            => Global.Native.type_is_a(gtype, type);

        //TODO: This should not be public
        public IntPtr GetClassPointer()
        {
            IntPtr ptr = TypeClass.Native.peek(Value);
            
            if(ptr == IntPtr.Zero)
                ptr = TypeClass.Native.@ref(Value);
            
            return ptr;
        }
        #endregion

        //Offsets see: https://gitlab.gnome.org/GNOME/glib/blob/master/gobject/gtype.h
    }

    internal enum Types
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
