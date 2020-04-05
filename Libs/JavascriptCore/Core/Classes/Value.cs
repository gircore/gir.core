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

        public bool HasProperty(string name) => JavaScriptCore.Value.object_has_property(this, name);
        public void InvokeMethod(string name)
        {
            var array = new GObject.Value[0];
            JavaScriptCore.Value.object_invoke_methodv(this, name, 0, array);
        }

        public Value GetProperty(string name) => new Value(JavaScriptCore.Value.object_get_property(this, name));
    }
}