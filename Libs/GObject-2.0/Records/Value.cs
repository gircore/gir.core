using System;
using System.Runtime.InteropServices;
using GLib;

namespace GObject
{
    public partial record Value : IDisposable
    {
        internal Native.ValueSafeHandle Handle { get; }
        
        #region Constructors

        internal Value(Native.ValueSafeHandle handle)
        {
            Handle = handle;
        }
        
        public Value(Type type)
        {
            Handle = Native.ManagedValueSafeHandle.Create();

            Native.Methods.Init(Handle, type.Value);
        }

        public Value(IntPtr value) : this(Type.Object) => Native.Methods.SetObject(Handle, value);
        public Value(bool value) : this(Type.Boolean) => Native.Methods.SetBoolean(Handle, value);
        public Value(int value) : this(Type.Int) => Native.Methods.SetInt(Handle, value);
        public Value(uint value) : this(Type.UInt) => Native.Methods.SetUint(Handle, value);
        public Value(long value) : this(Type.Long) => Native.Methods.SetLong(Handle, value);
        public Value(double value) : this(Type.Double) => Native.Methods.SetDouble(Handle, value);
        public Value(float value) : this(Type.Float) => Native.Methods.SetFloat(Handle, value);
        public Value(string value) : this(Type.String) => Native.Methods.SetString(Handle, value);

        #endregion

        #region Methods

        private Types GetTypeValue()
        {
            var structure = Marshal.PtrToStructure<Native.Struct>(Handle.DangerousGetHandle());
            return (Types)structure.GType;
        }
        
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
            Object obj => new Value(obj.Handle),
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
            var type = GetTypeValue();
            return type switch
            {
                Types.Boolean => GetBool(),
                Types.UInt => GetUint(),
                Types.Int => GetInt(),
                Types.Long => GetLong(),
                Types.Double => GetDouble(),
                Types.Float => GetFloat(),
                Types.String => GetString(),
                Types.Pointer => GetPtr(),
                _ => CheckComplexTypes(type)
            };
        }

        private object? CheckComplexTypes(Types gtype)
        {
            if (Functions.TypeIsA(gtype, Types.Object))
                return GetObject();

            if (Functions.TypeIsA(gtype, Types.Boxed))
                throw new NotImplementedException();

            if (Functions.TypeIsA(gtype, Types.Enum))
                return GetEnum();

            if (Functions.TypeIsA(gtype, Types.Flags))
                return GetFlags();

            throw new NotSupportedException($"Unable to extract the value to the given type. The type {gtype} is unknown.");
        }

        public T Extract<T>() => (T) Extract()!;

        public IntPtr GetPtr() => Native.Methods.GetPointer(Handle);
        public IntPtr GetBoxed() => Native.Methods.GetBoxed(Handle);

        public Object? GetObject()
            => null; //Object.TryWrapHandle(Native.Methods.GetObject(Handle), false, out Object? obj) ? obj : null;

        public bool GetBool() => Native.Methods.GetBoolean(Handle);
        public uint GetUint() => Native.Methods.GetUint(Handle);
        public int GetInt() => Native.Methods.GetInt(Handle);
        public long GetLong() => Native.Methods.GetLong(Handle);
        public double GetDouble() => Native.Methods.GetDouble(Handle);
        public float GetFloat() => Native.Methods.GetFloat(Handle);
        public long GetFlags() => Native.Methods.GetFlags(Handle);
        public long GetEnum() => Native.Methods.GetEnum(Handle);
        public string GetString() => Native.Methods.GetString(Handle);

        public void Dispose()
        {
            Handle.Dispose();
        }
        
        #endregion
        
    }
}
