using System;

namespace GObject.Core
{
    public abstract class GPropertyBase<T> : CanChange<T>
    {
        public event EventHandler<ChangedArgs<T>>? Changed;
        protected readonly string name;

        public GPropertyBase(GObject obj, string name)
        {
            this.name = name ?? throw new ArgumentNullException(nameof(name));
        }

        protected virtual void OnChanged(T value) => Changed?.Invoke(this, new ChangedArgs<T>(value));
    }
}