using System;
using System.Runtime.InteropServices;
using GObject;

namespace JavaScriptCore
{
    public class Value : GObject.Object
    {
        #region Properties
        public Property<Context> Context { get; }
        #endregion Properties
        public Value(IntPtr handle) : base(handle) 
        {
            Context = Property("context",
                get : GetObject<Context>,
                set : Set
            );
        }

        public double GetDouble() => Sys.Value.to_double(this);
        public int GetInt() => Sys.Value.to_int32(this);
        public string GetString() => Marshal.PtrToStringAuto(Sys.Value.to_string(this));
        public bool GetBool() => Sys.Value.to_boolean(this);

        public bool IsNumber() => Sys.Value.is_number(this);
        public bool IsString() => Sys.Value.is_string(this);
        public bool IsNull() => Sys.Value.is_null(this);
        public bool IsUndefined() => Sys.Value.is_undefined(this);
        public bool IsBool() => Sys.Value.is_boolean(this);
        public bool IsObject() => Sys.Value.is_object(this);
        public bool IsFunction() => Sys.Value.is_function(this);
        public bool IsArray() => Sys.Value.is_array(this);

        public bool HasProperty(string name) => Sys.Value.object_has_property(this, name);
        public Value InvokeMethod(string name)
        {
            var ptr = IntPtr.Zero;
            var ret = Sys.Value.object_invoke_methodv(this, name, 0, ref ptr);

            return new Value(ret);
        }

        public Value GetProperty(string name) => GetProperty(Sys.Value.object_get_property(this, name));

        public Value GetPropertyAtIndex(uint index) => GetProperty(Sys.Value.object_get_property_at_index(this, index));

        private static Value GetProperty(IntPtr ptr)
        {
            return TryGetObject(ptr, out Value obj) ? obj : new Value(ptr);
        }
    }
}