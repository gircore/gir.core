using System;
using System.Runtime.InteropServices;
using GObject.Core;

namespace JavaScriptCore.Core
{
    public class Value : GObject.Core.GObject
    {
        #region Properties
        public Property<Context> Context { get; }
        #endregion Properties
        public Value(IntPtr handle) : base(handle) 
        {
            Context = Property<Context>("context",
                get : GetObject<Context>,
                set : Set
            );
        }

        public double GetDouble() => JavaScriptCore.Value.to_double(this);
        public int GetInt() => JavaScriptCore.Value.to_int32(this);
        public string GetString() => Marshal.PtrToStringAuto(JavaScriptCore.Value.to_string(this));
        public bool GetBool() => JavaScriptCore.Value.to_boolean(this);

        public bool IsNumber() => JavaScriptCore.Value.is_number(this);
        public bool IsString() => JavaScriptCore.Value.is_string(this);
        public bool IsNull() => JavaScriptCore.Value.is_null(this);
        public bool IsUndefined() => JavaScriptCore.Value.is_undefined(this);
        public bool IsBool() => JavaScriptCore.Value.is_boolean(this);
        public bool IsObject() => JavaScriptCore.Value.is_object(this);
        public bool IsFunction() => JavaScriptCore.Value.is_function(this);
        public bool IsArray() => JavaScriptCore.Value.is_array(this);

        public bool HasProperty(string name) => JavaScriptCore.Value.object_has_property(this, name);
        public Value InvokeMethod(string name)
        {
            var array = new GObject.Value[0];
            var ret = JavaScriptCore.Value.object_invoke_methodv(this, name, 0, array);

            return new Value(ret);
        }

        public Value GetProperty(string name) => GetProperty(JavaScriptCore.Value.object_get_property(this, name));

        public Value GetPropertyAtIndex(uint index) => GetProperty(JavaScriptCore.Value.object_get_property_at_index(this, index));

        private Value GetProperty(IntPtr ptr)
        {
            if(TryGetObject(ptr, out Value? obj))
                return obj!;
            else
                return new Value(ptr);
        }
    }
}