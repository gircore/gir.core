using System;
using System.Runtime.InteropServices;

namespace JavaScriptCore.Core
{
    public class Value : GObject.Core.GObject
    {
        public Value(IntPtr handle) : base(handle) { }

        public double GetDouble() => JavaScriptCore.Value.to_double(this);
        public int GetInt() => JavaScriptCore.Value.to_int32(this);
        public string GetString() => Marshal.PtrToStringAuto(JavaScriptCore.Value.to_string(this));
        public bool GetBool() => JavaScriptCore.Value.to_boolean(this);

        public bool IsNumber() => JavaScriptCore.Value.is_number(this);
        public bool IsString() => JavaScriptCore.Value.is_string(this);
        public bool IsNull() => JavaScriptCore.Value.is_null(this);
        public bool IsUndefined() => JavaScriptCore.Value.is_undefined(this);
        public bool IsBool() => JavaScriptCore.Value.is_boolean(this);
    }
}