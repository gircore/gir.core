using System;

namespace JavaScriptCore.Core
{
    public class Value : GObject.Core.GObject
    {
        public Value(IntPtr handle) : base(handle)
        {
        }

        //public int ToInt() => JavaScriptCore.Value.to_int32(handle);
    }
}