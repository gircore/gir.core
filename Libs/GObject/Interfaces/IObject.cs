using System;

namespace GObject
{
    public interface IObject
    {
        private Object GetObject()
        {
            if (this is Object o)
                return o;
            
            throw new Exception($"{GetType().FullName} must inherit {typeof(Object)} to be allowed to be an {nameof(IObject)}.");
        }

        protected T GetProperty<T>(Property<T> property) 
            => GetObject().GetProperty(property);

        protected void SetProperty<T>(Property<T> property, T value)
            => GetObject().SetProperty(property, value);
        
    }
}
