using System;

namespace GObject
{
    public class GReadOnlyProperty<T> : NamedProperty<T>, ReadOnlyProperty<T>
    {
        protected readonly Func<string, T> get;

        public virtual T Value => get(name);

        public GReadOnlyProperty(Object obj, string name, Func<string, T> get) : base(obj, name)
        {
            this.get = get ?? throw new ArgumentNullException(nameof(get));

            obj.RegisterNotifyPropertyChangedEvent(name, () => base.OnChanged(this.get(name)));
        }
    }
}