namespace GObject
{
    using System;
    using System.Runtime.InteropServices;

    public struct Value : IDisposable
    {
        #region Fields

        internal Sys.Value GValue;

        #endregion

        #region Constructors

        public Value(bool value) : this(new Sys.Value(value)){ }

        public Value(int value) : this(new Sys.Value(value)) { }

        public Value(uint value) : this(new Sys.Value(value)) { }

        public Value(long value) : this(new Sys.Value(value)) { }

        public Value(double value) : this(new Sys.Value(value)) { }

        public Value(string value) : this(new Sys.Value(value)) { }

        internal Value(IntPtr value) : this(new Sys.Value(value)) { }

        internal Value(Sys.Value value)
            => GValue = value;

        #endregion

        #region Methods

        /// <inheritdoc cref="Sys.Value"/>
        public static Value From(object? value)
            => new Value(Sys.Value.From(value));

        /// <inheritdoc cref="Sys.Value"/>
        public T To<T>()
            => GValue.To<T>();

        internal IntPtr GetPointer() => Sys.Value.get_pointer(ref GValue);
        internal IntPtr GetBoxed() => Sys.Value.get_boxed(ref GValue);
        internal IntPtr GetObject() => Sys.Value.get_object(ref GValue);
        public bool GetBool() => Sys.Value.get_boolean(ref GValue);
        public uint GetUint() => Sys.Value.get_uint(ref GValue);
        public int GetInt() => Sys.Value.get_int(ref GValue);
        public long GetLong() => Sys.Value.get_long(ref GValue);
        public double GetDouble() => Sys.Value.get_double(ref GValue);

        public string GetString()
        {
            var ptr = Sys.Value.get_string(ref GValue);
            return Marshal.PtrToStringAnsi(ptr);
        }

        public void Dispose()
            => GValue.Dispose();

        #endregion
    }
}