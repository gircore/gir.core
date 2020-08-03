using System;

namespace JavaScriptCore
{
    public class Context : GObject.Object
    {
        internal Context(IntPtr handle) : base(handle) { }

        public void Throw(string errorMessage) => Sys.Context.@throw(this, errorMessage);
        public void SetValue(string name, Value value) => Sys.Context.set_value(this, name, value);
        public Value GetValue(string name) => new Value(Sys.Context.get_value(this, name));
        public Value GetGlobalObject() => new Value(Sys.Context.get_global_object(this));
    }
}