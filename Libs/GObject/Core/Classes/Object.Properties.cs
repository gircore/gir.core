using System;
using System.Runtime.CompilerServices;

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

            if (TryWrapPointerAs<T>(value.To<IntPtr>(), out var ret))
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
                SetGProperty(o.Handle, property.Name);
            else
                SetGProperty(Sys.Value.From(value), property.Name);
        }

        #endregion

        private void SetGProperty(Sys.Value value, string? propertyName)
        {
            ThrowIfDisposed();

            if (propertyName is null)
                return;

            Sys.Object.set_property(handle, propertyName, ref value);
            value.Dispose();
        }

        protected void Set(Object? value, [CallerMemberName] string? propertyName = null) => SetGProperty(value?.Handle ?? IntPtr.Zero, propertyName);
        protected void SetEnum<T>(T e, [CallerMemberName] string? propertyName = null) where T : Enum => SetGProperty((long)(object)e, propertyName);
        protected void Set(bool value, [CallerMemberName] string? propertyName = null) => SetGProperty(value, propertyName);
        protected void Set(uint value, [CallerMemberName] string? propertyName = null) => SetGProperty(value, propertyName);
        protected void Set(int value, [CallerMemberName] string? propertyName = null) => SetGProperty(value, propertyName);
        protected void Set(string value, [CallerMemberName] string? propertyName = null) => SetGProperty(value, propertyName);

        private Sys.Value GetGProperty(string? propertyName)
        {
            ThrowIfDisposed();

            if (propertyName is null)
                return default;

            var value = new Sys.Value();
            Sys.Object.get_property(handle, propertyName, ref value);

            return value;
        }

        protected T GetEnum<T>([CallerMemberName] string? propertyName = null) where T : Enum
        {
            using var v = GetGProperty(propertyName);
            return (T)((object)((long)v));
        }

        protected int GetInt([CallerMemberName] string? propertyName = null)
        {
            using var v = GetGProperty(propertyName);
            return (int)v;
        }

        protected bool GetBool([CallerMemberName] string? propertyName = null)
        {
            using var v = GetGProperty(propertyName);
            return (bool)v;
        }
        protected double GetDouble([CallerMemberName] string? propertyName = null)
        {
            using var v = GetGProperty(propertyName);
            return (double)v;
        }

        protected uint GetUInt([CallerMemberName] string? propertyName = null)
        {
            using var v = GetGProperty(propertyName);
            return (uint)v;
        }

        protected string GetStr([CallerMemberName] string? propertyName = null)
        {
            using var v = GetGProperty(propertyName);
            return (string)v;
        }

        protected IntPtr GetIntPtr([CallerMemberName] string? propertyName = null)
        {
            using var v = GetGProperty(propertyName);
            return (IntPtr)v;
        }

        ///<summary>
        ///May return null!
        ///</sumamry>
        protected T GetObject<T>([CallerMemberName] string? propertyName = null) where T : Object
        {
            var v = GetIntPtr(propertyName);

            return WrapPointerAs<T>(v);
        }
    }
}