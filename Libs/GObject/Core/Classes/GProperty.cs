using System;

namespace GObject
{
    public class GProperty<T> : NamedProperty<T>, Property<T>
    {
        protected readonly Action<T, string> set;
        protected readonly Func<string, T> get;

        public virtual T Value
        {
            get => get(name);
            set => set(value, name);
        }

        public GProperty(Object obj, string name, Func<string, T> get, Action<T, string> set) : base(obj, name)
        {
            this.get = get ?? throw new ArgumentNullException(nameof(get));
            this.set = set ?? throw new ArgumentNullException(nameof(set));

            obj.RegisterNotifyPropertyChangedEvent(name, () => base.OnChanged(Value));
        }
    }
}