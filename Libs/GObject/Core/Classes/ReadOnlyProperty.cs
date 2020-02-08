using System;

namespace GObject.Core
{
    public class GReadOnlyProperty<T> : GPropertyBase<T>, ReadOnlyProperty<T>
    {
        protected readonly Func<string, T> get;

        public virtual T Value => get(name);

        public GReadOnlyProperty(GObject obj, string name, Func<string, T> get) : base(obj, name)
        {
            this.get = get ?? throw new ArgumentNullException(nameof(get));

            obj.RegisterNotifyPropertyChangedEvent(name, () => base.OnChanged(this.get(name)));
        }
    }
}