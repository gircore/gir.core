using System;
using System.Runtime.InteropServices;
using GLib.Native;

namespace GObject
{
    public partial record Value : IDisposable
    {
        internal Native.Value.Handle Handle { get; }

        #region Constructors

        internal Value(Native.Value.Handle handle)
        {
            Handle = handle;
        }
        
        public Value(Type type)
        {
            var h = Native.Value.ManagedHandle.Create();
            Handle = Native.Value.Methods.Init(h, type.Value);
        }

        public Value(IntPtr value) : this(Type.Object) => Native.Value.Methods.SetObject(Handle, value);
        public Value(bool value) : this(Type.Boolean) => Native.Value.Methods.SetBoolean(Handle, value);
        public Value(int value) : this(Type.Int) => Native.Value.Methods.SetInt(Handle, value);
        public Value(uint value) : this(Type.UInt) => Native.Value.Methods.SetUint(Handle, value);
        public Value(long value) : this(Type.Long) => Native.Value.Methods.SetLong(Handle, value);
        public Value(double value) : this(Type.Double) => Native.Value.Methods.SetDouble(Handle, value);
        public Value(float value) : this(Type.Float) => Native.Value.Methods.SetFloat(Handle, value);
        public Value(string value) : this(Type.String) => Native.Value.Methods.SetString(Handle, value);

        #endregion

        #region Methods

        internal Native.Value.Struct GetData() => Marshal.PtrToStructure<Native.Value.Struct>(Handle.DangerousGetHandle());
        
        private ulong GetTypeValue()
        {
            var structure = Marshal.PtrToStructure<Native.Value.Struct>(Handle.DangerousGetHandle());
            return structure.GType;
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

        public void Set(object? value)
        {
            switch (value)
            {
                case bool b: 
                    SetBoolean(b); 
                    break;
                case uint u: 
                    SetUInt(u); 
                    break;
                case int i: 
                    SetInt(i); 
                    break;
                case string s: 
                    SetString(s); 
                    break;
                case double d: 
                    SetDouble(d); 
                    break;
                case string[] array:
                    SetBoxed(new StringArrayNullTerminatedSafeHandle(array).DangerousGetHandle());
                    break;
            }
        }
        
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
            return (Types) type switch
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

        private object? CheckComplexTypes(ulong gtype)
        {
            if (Functions.TypeIsA(gtype, Types.Object))
                return GetObject();

            if (Functions.TypeIsA(gtype, Types.Boxed))
                return GetBoxed(gtype);

            if (Functions.TypeIsA(gtype, Types.Enum))
                return GetEnum();

            if (Functions.TypeIsA(gtype, Types.Flags))
                return GetFlags();

            throw new NotSupportedException($"Unable to extract the value to the given type. The type {gtype} is unknown.");
        }

        public T Extract<T>() => (T) Extract()!;

        public IntPtr GetPtr() => Native.Value.Methods.GetPointer(Handle);

        public object? GetBoxed(ulong type)
        {
            //TODO Currently a boxed value is assumed to be a string array which is not always true.
            //var strvType = GLib.Native.Functions.StrvGetType(); //Method can not be called to check string array type
            var ptr = Native.Value.Methods.GetBoxed(Handle);
            return StringHelper.ToStringArray(ptr);
        }

        public Object? GetObject()
            => null; //Object.TryWrapHandle(Native.Methods.GetObject(Handle), false, out Object? obj) ? obj : null;

        public bool GetBool() => Native.Value.Methods.GetBoolean(Handle);
        public uint GetUint() => Native.Value.Methods.GetUint(Handle);
        public int GetInt() => Native.Value.Methods.GetInt(Handle);
        public long GetLong() => Native.Value.Methods.GetLong(Handle);
        public double GetDouble() => Native.Value.Methods.GetDouble(Handle);
        public float GetFloat() => Native.Value.Methods.GetFloat(Handle);
        public long GetFlags() => Native.Value.Methods.GetFlags(Handle);
        public long GetEnum() => Native.Value.Methods.GetEnum(Handle);
        public string GetString() => StringHelper.ToAutoString(Native.Value.Methods.GetString(Handle));

        private void SetBoxed(IntPtr ptr) => Native.Value.Methods.SetBoxed(Handle, ptr);
        private void SetBoolean(bool b) => Native.Value.Methods.SetBoolean(Handle, b);
        private void SetUInt(uint u) => Native.Value.Methods.SetUint(Handle, u);
        private void SetInt(int i) => Native.Value.Methods.SetInt(Handle, i);
        private void SetDouble(double d) => Native.Value.Methods.SetDouble(Handle, d);
        private void SetString(string s) => Native.Value.Methods.SetString(Handle, s);

        public void Dispose()
        {
            Handle.Dispose();
        }
        
        #endregion
        
    }
}
