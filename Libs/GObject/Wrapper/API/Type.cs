using System;
using System.Runtime.InteropServices;

namespace GObject
{
    [StructLayout(LayoutKind.Explicit, Size=8)]
    public struct Type
    {
        [FieldOffset(0)]
        private IntPtr value;

        #region Statics
        public static readonly Type Invalid = new Type ((IntPtr) Types.Invalid);
        public static readonly Type None = new Type ((IntPtr) Types.None);
        public static readonly Type Interface = new Type ((IntPtr) Types.Interface);
        public static readonly Type Char = new Type ((IntPtr) Types.Char);
        public static readonly Type UChar = new Type ((IntPtr) Types.UChar);
        public static readonly Type Boolean = new Type ((IntPtr) Types.Boolean);
        public static readonly Type Int = new Type ((IntPtr) Types.Int);
        public static readonly Type UInt = new Type ((IntPtr) Types.UInt);
        public static readonly Type Long = new Type ((IntPtr) Types.Long);
        public static readonly Type ULong = new Type ((IntPtr) Types.ULong);
        public static readonly Type Int64 = new Type ((IntPtr) Types.Int64);
        public static readonly Type UInt64 = new Type ((IntPtr) Types.UInt64);
        public static readonly Type Enum = new Type ((IntPtr) Types.Enum);
        public static readonly Type Flags = new Type ((IntPtr) Types.Flags);
        public static readonly Type Float = new Type ((IntPtr) Types.Float);
        public static readonly Type Double = new Type ((IntPtr) Types.Double);
        public static readonly Type String = new Type ((IntPtr) Types.String);
        public static readonly Type Pointer = new Type ((IntPtr) Types.Pointer);
        public static readonly Type Boxed = new Type ((IntPtr) Types.Boxed);
        public static readonly Type Param = new Type ((IntPtr) Types.Param);
        public static readonly Type Object = new Type ((IntPtr) Types.Object);
        public static readonly Type Variant = new Type ((IntPtr) Types.Variant);
        #endregion Statics

        internal Type(IntPtr value)
        {
            this.value = value;
        }

        /*public IntPtr GetClassPointer()
        {
            var ptr = TypeClass.peek(value);
            
            if(ptr == IntPtr.Zero)
                ptr = TypeClass.@ref(value);

            return ptr;
        }*/

        public static implicit operator IntPtr (Type type) => type.value;
		
        //Offsets see: https://gitlab.gnome.org/GNOME/glib/blob/master/gobject/gtype.h
        private enum Types 
        {
		    Invalid = 0 << 2,
            None = 1 << 2,
            Interface = 2 << 2,
            Char = 3 << 2,
            UChar = 4 << 2,
            Boolean = 5 << 2,
            Int	 = 6 << 2,
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
}