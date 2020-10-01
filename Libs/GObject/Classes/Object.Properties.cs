using System;

namespace GObject
{
    public partial class Object
    {
        #region Methods

        /// <summary>
        /// Gets the value of the GProperty described by <paramref name="property"/>.
        /// </summary>
        /// <param name="property">The property descriptor of the GProperty from which get the value.</param>
        /// <typeparam name="T">The type of the value to retrieve.</typeparam>
        /// <returns>
        /// The value of the GProperty.
        /// </returns>
        protected T GetProperty<T>(Property<T> property)
        {
            var value = GetGProperty(property.Name);

            if (TryWrapPointerAs(value.To<IntPtr>(), out T ret))
                return ret;

            return value.To<T>();
        }

        /// <summary>
        /// Sets the <paramref name="value"/> of the GProperty described by <paramref name="property"/>.
        /// </summary>
        /// <param name="property">The property descriptor of the GProperty on which set the value.</param>
        /// <param name="value">The value to set to the GProperty.</param>
        /// <typeparam name="T">The tye of the value to define.</typeparam>
        protected void SetProperty<T>(Property<T> property, T value)
        {
            if (value is Object o)
                SetGProperty(new Value(o.Handle), property.Name);
            else
                SetGProperty(Value.From(value), property.Name);
        }

        /// <summary>
        /// Defines the value of a native GProperty given its <paramref name="name"/>
        /// </summary>
        /// <param name="value">The property name.</param>
        /// <param name="name">The property value.</param>
        private void SetGProperty(Value value, string? name)
        {
            ThrowIfDisposed();

            if (name is null)
                return;

            Object.set_property(Handle, name, ref value);
            value.Dispose();
        }

        /// <summary>
        /// Gets the value of a native GProperty given its <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The property name.</param>
        /// <returns>
        /// The native value of the property, wrapped is a <see cref="Sys.Value"/>.
        /// </returns>
        private Value GetGProperty(string? name)
        {
            ThrowIfDisposed();

            if (name is null)
                return default;

            var value = new Value();
            Object.get_property(Handle, name, ref value);

            return value;
        }

        #endregion
    }
}