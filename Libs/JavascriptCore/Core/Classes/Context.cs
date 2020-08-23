using System;

namespace JavaScriptCore
{
    public partial class Context
    {
        public void Throw(string errorMessage) => Sys.Context.@throw(Handle, errorMessage);
        public void SetValue(string name, Value value) => Sys.Context.set_value(Handle, name, GetHandle(value));
        public Value GetValue(string name) => new Value(Sys.Context.get_value(Handle, name));
        public Value GetGlobalObject() => new Value(Sys.Context.get_global_object(Handle));
    }
}