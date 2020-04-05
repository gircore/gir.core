using System;

namespace JavaScriptCore.Core
{
    public class Context : GObject.Core.GObject
    {
        internal Context(IntPtr handle) : base(handle)
        {
        }

        public void Throw(string errorMessage) => JavaScriptCore.Context.@throw(this, errorMessage);
        public void SetValue(string name, Value value) => JavaScriptCore.Context.set_value(this, name, value);
        public Value GetValue(string name) => new Value(JavaScriptCore.Context.get_value(this, name));
        public Value GetGlobalObject() => new Value(JavaScriptCore.Context.get_global_object(this));
    }
}