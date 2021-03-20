using System;
using System.Runtime.InteropServices;
using GLib;

namespace GObject
{
    public partial record Value : IDisposable
    {
        private Native.ValueSafeHandle handle;
        
        #region Constructors

        public Value(Type type)
        {
            handle = Native.ManagedValueSafeHandle.Create();

            Native.Methods.Init(handle, (IntPtr)type.Value);
        }

        public Value(IntPtr value) : this(Type.Object) => Native.Methods.SetObject(handle, value);
        public Value(bool value) : this(Type.Boolean) => Native.Methods.SetBoolean(handle, value);
        public Value(int value) : this(Type.Int) => Native.Methods.SetInt(handle, value);
        public Value(uint value) : this(Type.UInt) => Native.Methods.SetUint(handle, value);
        public Value(long value) : this(Type.Long) => Native.Methods.SetLong(handle, value);
        public Value(double value) : this(Type.Double) => Native.Methods.SetDouble(handle, value);
        public Value(float value) : this(Type.Float) => Native.Methods.SetFloat(handle, value);
        public Value(string value) : this(Type.String) => Native.Methods.SetString(handle, value);

        #endregion

        #region Methods

        private Types GetTypeValue()
        {
            var structure = Marshal.PtrToStructure<Native.Struct>(handle.DangerousGetHandle());
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
            var ptr = (IntPtr) gtype;
            
            if (Functions.Native.TypeIsA(ptr, (IntPtr) Types.Object))
                return GetObject();

            if (Functions.Native.TypeIsA(ptr, (IntPtr) Types.Boxed))
                throw new NotImplementedException();

            if (Functions.Native.TypeIsA(ptr, (IntPtr) Types.Enum))
                return GetEnum();

            if (Functions.Native.TypeIsA(ptr, (IntPtr) Types.Flags))
                return GetFlags();

            throw new NotSupportedException($"Unable to extract the value to the given type. The type {gtype} is unknown.");
        }

        public T Extract<T>() => (T) Extract()!;

        public IntPtr GetPtr() => Native.Methods.GetPointer(handle);
        public IntPtr GetBoxed() => Native.Methods.GetBoxed(handle);

        public Object? GetObject()
            => Object.TryWrapHandle(Native.Methods.GetObject(handle), false, out Object? obj) ? obj : null;

        public bool GetBool() => Native.Methods.GetBoolean(handle);
        public uint GetUint() => Native.Methods.GetUint(handle);
        public int GetInt() => Native.Methods.GetInt(handle);
        public long GetLong() => Native.Methods.GetLong(handle);
        public double GetDouble() => Native.Methods.GetDouble(handle);
        public float GetFloat() => Native.Methods.GetFloat(handle);
        public long GetFlags() => Native.Methods.GetFlags(handle);
        public long GetEnum() => Native.Methods.GetEnum(handle);
        public string GetString() => Native.Methods.GetString(handle);

        public void Dispose()
        {
            handle.Dispose();
        }
        
        #endregion
        
    }
}
