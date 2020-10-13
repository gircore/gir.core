using System;
using System.Runtime.InteropServices;

namespace GObject
{
    public partial struct Value : IDisposable
    {
        #region Constructors

        public Value(Type type)
        {
            g_type = 0;
            data = IntPtr.Zero;
            data2 = IntPtr.Zero;

            Value.init(ref this, type.Value);
        }

        public Value(IntPtr value) : this(Type.Object) => Value.set_object(ref this, value);
        public Value(bool value) : this(Type.Boolean) => Value.set_boolean(ref this, value);
        public Value(int value) : this(Type.Int) => Value.set_int(ref this, value);
        public Value(uint value) : this(Type.UInt) => Value.set_uint(ref this, value);
        public Value(long value) : this(Type.Long) => Value.set_long(ref this, value);
        public Value(double value) : this(Type.Double) => Value.set_double(ref this, value);
        public Value(string value) : this(Type.String) => Value.set_string(ref this, value);

        #endregion
        
        #region Methods
        
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
            Enum _ => new Value((long) value),
            _ => throw new NotSupportedException("Unable to create the value from the given type.")
        };

        /// <summary>
        /// Casts this <see cref="Value"/> to the type <typeparamref name="T"/>.
        ///
        /// In case of an IntPtr a GObject pointer is returned. This method does not support GPointer.
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
            if (t == typeof(bool)) return (T) (object) GetBool();
            if (t == typeof(uint)) return (T) (object) GetUint();
            if (t == typeof(int)) return (T) (object) GetInt();
            if (t.IsEnum || t == typeof(long)) return (T) (object) GetLong();
            if (t == typeof(double)) return (T) (object) GetDouble();
            if (t == typeof(string)) return (T) (object) GetString();
            
            //Warning: This could be GetPointer() or GetObject()!
            if (t == typeof(IntPtr)) return (T) (object) GetObject(); 

            throw new NotSupportedException("Unable to cast the value to the given type.");
        }

        public object Extract()
        {
            return g_type switch
            {
                (ulong) Types.Boolean => GetBool(),
                (ulong) Types.UInt => GetUint(),
                (ulong) Types.Int => GetInt(),
                (ulong) Types.Enum => GetLong(),
                (ulong) Types.Long => GetLong(),
                (ulong) Types.Double => GetDouble(),
                (ulong) Types.String => GetString(),
                (ulong) Types.Object => GetObject(), //TODO: Get real Object
                (ulong) Types.Pointer => GetPtr(),
                _ => throw new NotSupportedException($"Unable to extract the value to the given type. The type {g_type} is unknown.")
            };
        }
        
        public IntPtr GetPtr() => Value.get_pointer(ref this);
        public IntPtr GetBoxed() => Value.get_boxed(ref this);
        public IntPtr GetObject() => Value.get_object(ref this);
        public bool GetBool() => Value.get_boolean(ref this);
        public uint GetUint() => Value.get_uint(ref this);
        public int GetInt() => Value.get_int(ref this);
        public long GetLong() => Value.get_long(ref this);
        public double GetDouble() => Value.get_double(ref this);

        public string GetString()
        {
            var ptr = Value.get_string(ref this);
            return Marshal.PtrToStringAnsi(ptr);
        }
        
        public void Dispose() => Value.unset(ref this);
        
        #endregion
    }
}