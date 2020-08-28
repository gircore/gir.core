using System;
using System.Runtime.InteropServices;

namespace GObject.Sys
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

            Value.init(ref this, type.Value);
        }

        public void Dispose() => Value.unset(ref this);

        public Value(IntPtr value) : this(Type.Object) => Value.set_object(ref this, value);
        public Value(bool value) : this(Type.Boolean) => Value.set_boolean(ref this, value);
        public Value(int value) : this(Type.Int) => Value.set_int(ref this, value);
        public Value(uint value) : this(Type.UInt) => Value.set_uint(ref this, value);
        public Value(long value) : this(Type.Long) => Value.set_long(ref this, value);
        public Value(double value) : this(Type.Double) => Value.set_double(ref this, value);
        public Value(string value) : this(Type.String) => Value.set_string(ref this, value);

        /// <summary>
        /// Gets an instance of <see cref="Value"/> from the given <paramref name="value"/>.
        /// </summary>
        /// <returns>
        /// An instance of <see cref="Value"/> if the cast is successful.
        /// </returns>
        /// <exception cref="NotSupportedException">
        /// The given <paramref name="value"/> has a type which cannot be parsed as a <see cref="Value"/>.
        /// </exception>
        public static Value From(object? value) => value switch
        {
            null => new Value(IntPtr.Zero),
            bool v1 => new Value(v1),
            uint v2 => new Value(v2),
            int v3 => new Value(v3),
            long v4 => new Value(v4),
            double v5 => new Value(v5),
            string v6 => new Value(v6),
            IntPtr v7 => new Value(v7),
            Enum _ => new Value((long)value),
            _ => throw new NotSupportedException("Unable to create the value from the given type.")
        };

        /// <summary>
        /// Casts this <see cref="Value"/> to the type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type in which this value is casted.</typeparam>
        /// <returns>
        /// A value of type <typeparamref name="T"/> if the cast is successful.
        /// </returns>
        /// <exception cref="NotSupportedException">
        /// The value cannot be casted to the given type.
        /// </exception>
        public T To<T>()
        {
            System.Type t = typeof(T);

            if (t == typeof(bool)) return (T)(object)(bool)this;
            if (t == typeof(uint)) return (T)(object)(uint)this;
            if (t == typeof(int)) return (T)(object)(int)this;
            if (t.IsEnum || t == typeof(long)) return (T)(object)(long)this;
            if (t == typeof(double)) return (T)(object)(double)this;
            if (t == typeof(string)) return (T)(object)(string)this;
            if (t == typeof(IntPtr)) return (T)(object)(IntPtr)this;

            throw new NotSupportedException("Unable to cast the value to the given type.");
        }

        //TODO: Explicite / Implicite operatoren entfernen?
        public IntPtr GetPtr() => Value.get_pointer(ref this);
        public IntPtr GetBoxed() => Value.get_boxed(ref this);
        public IntPtr GetObject() => Value.get_object(ref this);

        public static explicit operator IntPtr(Value value) => Value.get_object(ref value);
        public static explicit operator bool(Value value) => Value.get_boolean(ref value);
        public static explicit operator uint(Value value) => Value.get_uint(ref value);
        public static explicit operator int(Value value) => Value.get_int(ref value);
        public static explicit operator long(Value value) => Value.get_long(ref value);
        public static explicit operator double(Value value) => Value.get_double(ref value);
        public static explicit operator string(Value value)
        {
            var ptr = Value.get_string(ref value);
            return Marshal.PtrToStringAnsi(ptr);
        }

        public static implicit operator Value(IntPtr value) => new Value(value);
        public static implicit operator Value(long value) => new Value(value);
        public static implicit operator Value(bool value) => new Value(value);
        public static implicit operator Value(uint value) => new Value(value);
        public static implicit operator Value(int value) => new Value(value);
        public static implicit operator Value(string value) => new Value(value);
    }
}