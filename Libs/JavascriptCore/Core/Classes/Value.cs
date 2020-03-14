using System;
using System.Runtime.InteropServices;

namespace JavaScriptCore.Core
{
    public class Value : GObject.Core.GObject
    {
        public Value(IntPtr handle) : base(handle)
        {
        }

        public int GetInt() => JavaScriptCore.Value.to_int32(this);
        public string GetString() => Marshal.PtrToStringAuto(JavaScriptCore.Value.to_string(this));
    }
}