using System;

namespace GObject.Core
{
    public partial class GObject
    {
        protected Property<T> Property<T>(string name, Func<string, T> get, Action<T, string> set) => new GProperty<T>(this, name, get, set);
        protected ReadOnlyProperty<T> ReadOnlyProperty<T>(string name, Func<string, T> get) => new GReadOnlyProperty<T>(this, name, get);
        protected Property<T1> Property<T1, T2>(string name, Func<string, T2> get, Action<T2, string> set, System.Converter<T1, T2> to, System.Converter<T2, T1> from) => new GConvertingProperty<T1, T2>(this, name, get, set, to, from);
    }
}