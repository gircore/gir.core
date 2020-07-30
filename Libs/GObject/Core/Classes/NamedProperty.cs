using System;

namespace GObject
{
    public abstract class NamedProperty<T> : CanChange<T>
    {
        public event EventHandler<ChangedArgs<T>>? Changed;
        protected readonly string name;

        public NamedProperty(Object obj, string name)
        {
            this.name = name ?? throw new ArgumentNullException(nameof(name));
        }

        protected virtual void OnChanged(T value) => Changed?.Invoke(this, new ChangedArgs<T>(value));
    }
}