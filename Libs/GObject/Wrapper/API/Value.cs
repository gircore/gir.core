using System;
using System.Runtime.InteropServices;

namespace GObject
{
    [StructLayout(LayoutKind.Sequential)]
    public partial struct Value : IDisposable
    {
        IntPtr type;

        long data1;
        long data2;

        public Value(Type type)
        {
            this.type = IntPtr.Zero;
            data1 = 0;
            data2 = 0;

            Value.init(ref this, type);
        }

        public void Dispose() => Value.unset(ref this);

        public Value(IntPtr value) : this(Type.Object) => Value.set_object(ref this, value);
        public Value(bool value) : this(Type.Boolean) => Value.set_boolean(ref this, value);
        public Value(int value) : this(Type.Int) => Value.set_int(ref this, value);
        public Value(uint value) : this(Type.UInt) => Value.set_uint(ref this, value);
        public Value(long value) : this(Type.Long) => Value.set_long(ref this, value);
        public Value(string value) : this(Type.String) => Value.set_string(ref this, value);

        //TODO: Explicite / Implicite operatoren entfernen?
        public IntPtr GetPtr() => Value.get_pointer(ref this);
        public IntPtr GetBoxed() => Value.get_boxed(ref this);
        public IntPtr GetObject() => Value.get_object(ref this);

        public static explicit operator IntPtr (Value value) => Value.get_object(ref value);
        public static explicit operator bool (Value value) => Value.get_boolean(ref value);
        public static explicit operator uint (Value value) => Value.get_uint(ref value);
        public static explicit operator int (Value value) => Value.get_int(ref value);
        public static explicit operator long (Value value) => Value.get_long(ref value);
        public static explicit operator string (Value value) 
        {
            var ptr = Value.get_string(ref value);
            return Marshal.PtrToStringAnsi(ptr);
        }

        public static implicit operator Value(IntPtr value) => new Value(value);
        public static implicit operator Value (long value) => new Value(value);
        public static implicit operator Value (bool value) => new Value(value);
        public static implicit operator Value (uint value) => new Value(value);
        public static implicit operator Value (int value) => new Value(value);
        public static implicit operator Value (string value) => new Value(value);
    }
}