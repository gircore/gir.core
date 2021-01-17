using System;
using System.Runtime.InteropServices;

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
            => GetProperty(property.Name).Extract<T>();

        /// <summary>
        /// Sets the <paramref name="value"/> of the GProperty described by <paramref name="property"/>.
        /// </summary>
        /// <param name="property">The property descriptor of the GProperty on which set the value.</param>
        /// <param name="value">The value to set to the GProperty.</param>
        /// <typeparam name="T">The tye of the value to define.</typeparam>
        protected void SetProperty<T>(Property<T> property, T value)
        {
            if (value is Object o)
                SetProperty(property.Name, new Value(o.Handle));
            else
                SetProperty(property.Name, Value.From(value));
        }

        /// <summary>
        /// Assigns the value of a GObject's property given its <paramref name="name"/>
        /// </summary>
        /// <param name="value">The property name.</param>
        /// <param name="name">The property value.</param>
        protected void SetProperty(string? name, Value value)
        {
            ThrowIfDisposed();

            if (name is null)
                return;

            Native.set_property(Handle, name, ref value);
            value.Dispose();
        }

        /// <summary>
        /// Gets the value of a GObject's property given its <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The property name.</param>
        /// <returns>
        /// The native value of the property, wrapped as a <see cref="Value"/>.
        /// </returns>
        protected Value GetProperty(string? name)
        {
            ThrowIfDisposed();

            if (name is null)
                return default;

            var value = new Value();
            Native.get_property(Handle, name, ref value);

            return value;
        }

        protected static void InstallProperty<T>(uint id, IntPtr objectClass, Property<T> property)
        {
            var spec = GetParamSpec(property);   
            ObjectClass.Native.install_property(objectClass, id, spec);
        }

        private static IntPtr GetParamSpec<T>(Property<T> property)
        {
            var name = property.Name;
            var nick = property.PropertyName;
            var blurb = "The " + nick + " property";

            ParamFlags flags = default;

            if (property.IsReadable)
                flags |= ParamFlags.Readable;
            if (property.IsWriteable)
                flags |= ParamFlags.Writable;

            return property.Kind switch
            {
                Types.Enum => Global.Native.param_spec_enum(name, nick, blurb, GetType(property), 0, flags),
                _ => throw new NotSupportedException("Unknown property type")
            };
        }

        private static ulong GetType<T>(Property<T> property)
        {
            Type? type = property.GetGType();
            if (type is null)
                throw new Exception($"Can not register property {property.Name}. Type is not specified");

            return type.Value.Value;
        }

        #endregion
    }
}
