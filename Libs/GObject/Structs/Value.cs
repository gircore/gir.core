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
            data1 = IntPtr.Zero;
            data2 = IntPtr.Zero;

            Native.init(ref this, type.Value);
        }

        public Value(IntPtr value) : this(Type.Object) => Native.set_object(ref this, value);
        public Value(bool value) : this(Type.Boolean) => Native.set_boolean(ref this, value);
        public Value(int value) : this(Type.Int) => Native.set_int(ref this, value);
        public Value(uint value) : this(Type.UInt) => Native.set_uint(ref this, value);
        public Value(long value) : this(Type.Long) => Native.set_long(ref this, value);
        public Value(double value) : this(Type.Double) => Native.set_double(ref this, value);
        public Value(float value) : this(Type.Float) => Native.set_float(ref this, value);
        public Value(string value) : this(Type.String) => Native.set_string(ref this, value);

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
            float v6 => new Value(v6),
            string v7 => new Value(v7),
            IntPtr v8 => new Value(v8),
            Enum _ => new Value((long) value),
            _ => throw new NotSupportedException("Unable to create the value from the given type.")
        };

        /// <summary>
        /// Extracts the content of this <see cref="Value"/> into an object.
        /// </summary>
        /// <returns>The content of this wrapped in an object</returns>
        /// <exception cref="NotSupportedException">
        /// The value cannot be casted to the given type.
        /// </exception>
        public object? Extract()
        {
            return g_type switch
            {
                (ulong) Types.Boolean => GetBool(),
                (ulong) Types.UInt => GetUint(),
                (ulong) Types.Int => GetInt(),
                (ulong) Types.Enum => GetLong(),
                (ulong) Types.Long => GetLong(),
                (ulong) Types.Double => GetDouble(),
                (ulong) Types.Float => GetFloat(),
                (ulong) Types.String => GetString(),
                (ulong) Types.Pointer => GetPtr(),
                _ => CheckComplexTypes(g_type)
            };
        }

        private object? CheckComplexTypes(ulong gtype)
        {
            if (Global.Native.type_is_a(gtype, (ulong) Types.Object))
                return GetObject();

            if (Global.Native.type_is_a(gtype, (ulong) Types.Boxed))
                throw new NotImplementedException();

            if (Global.Native.type_is_a(gtype, (ulong) Types.Enum))
                return GetEnum();

            if (Global.Native.type_is_a(gtype, (ulong) Types.Flags))
                return GetFlags();
            
            throw new NotSupportedException($"Unable to extract the value to the given type. The type {gtype} is unknown.");
        }

        public T Extract<T>() => (T) Extract()!;

        public IntPtr GetPtr() => Native.get_pointer(ref this);
        public IntPtr GetBoxed() => Native.get_boxed(ref this);

        public Object? GetObject()
            => Object.TryWrapPointerAs(Native.get_object(ref this), out Object obj) ? obj : null;

        public bool GetBool() => Native.get_boolean(ref this);
        public uint GetUint() => Native.get_uint(ref this);
        public int GetInt() => Native.get_int(ref this);
        public long GetLong() => Native.get_long(ref this);
        public double GetDouble() => Native.get_double(ref this);
        public float GetFloat() => Native.get_float(ref this);
        public uint GetFlags() => Native.get_flags(ref this);
        public int GetEnum() => Native.get_enum(ref this);

        public string GetString()
        {
            IntPtr ptr = Native.get_string(ref this);
            return Marshal.PtrToStringAnsi(ptr);
        }

        #endregion

        #region IDisposable Implementation

        public void Dispose() => Native.unset(ref this);

        #endregion
    }
}
