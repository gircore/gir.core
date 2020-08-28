using System;
using System.Runtime.InteropServices;
using GObject;

namespace JavaScriptCore
{
    public partial class Value
    {
        #region Properties
        public IProperty<Context> Context { get; }
        #endregion Properties

        #region Methods
        public double GetDouble() => Sys.Value.to_double(Handle);
        public int GetInt() => Sys.Value.to_int32(Handle);
        public string GetString() => Marshal.PtrToStringAuto(Sys.Value.to_string(Handle));
        public bool GetBool() => Sys.Value.to_boolean(Handle);

        public bool IsNumber() => Sys.Value.is_number(Handle);
        public bool IsString() => Sys.Value.is_string(Handle);
        public bool IsNull() => Sys.Value.is_null(Handle);
        public bool IsUndefined() => Sys.Value.is_undefined(Handle);
        public bool IsBool() => Sys.Value.is_boolean(Handle);
        public bool IsObject() => Sys.Value.is_object(Handle);
        public bool IsFunction() => Sys.Value.is_function(Handle);
        public bool IsArray() => Sys.Value.is_array(Handle);

        public bool HasProperty(string name) => Sys.Value.object_has_property(Handle, name);
        public Value InvokeMethod(string name)
        {
            var ptr = IntPtr.Zero;
            var ret = Sys.Value.object_invoke_methodv(Handle, name, 0, ref ptr);

            return new Value(ret);
        }

        public Value GetProperty(string name) => GetProperty(Sys.Value.object_get_property(Handle, name));

        public Value GetPropertyAtIndex(uint index) => GetProperty(Sys.Value.object_get_property_at_index(Handle, index));

        private static Value GetProperty(IntPtr ptr)
        {
            return WrapPointerAs<Value>(ptr);
        }
        #endregion

        public static Value Create(IntPtr ptr) => new Value(ptr);
    }
}