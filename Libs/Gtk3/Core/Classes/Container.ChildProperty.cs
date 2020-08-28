using System;

namespace Gtk
{
    public partial class Container
    {
        #region ChildProperty<T> Implementation

        /// <summary>
        /// Child GProperty descriptor.
        /// </summary>
        /// <typeparam name="T">The type of the value this child property will store.</typeparam>
        public sealed class ChildProperty<T>
        {
            #region Properties

            /// <summary>
            /// The name of the GProperty.
            /// </summary>
            public string Name { get; }

            #endregion

            #region Constructors

            /// <summary>
            /// Initializes a new instance of <see cref="ChildProperty{T}"/>.
            /// </summary>
            /// <remarks>
            /// This constructor is made private, since the end user need
            /// to register his properties using <see cref="Register{TObject}"/>.
            /// </remarks>
            /// <param name="name">The name of this child GProperty</param>
            private ChildProperty(string name)
            {
                Name = name;
            }

            #endregion

            #region Methods

            /// <summary>
            /// Registers this child property descriptor and creates the correct child GProperty
            /// into the GLib type of <typeparamref name="TObject"/>.
            /// </summary>
            /// <param name="name">The name of the child GProperty to create.</param>
            /// <returns>
            /// An instance of <see cref="ChildProperty{T}"/> representing the child GProperty description.
            /// </returns>
            public static ChildProperty<T> Register(string name)
            {
                return new ChildProperty<T>(name);
            }

            #endregion
        }

        #endregion

        #region Methods

        /// <summary>
        /// Updates the child <paramref name="property"/> to the given <paramref name="value"/>
        /// in the <paramref name="widget"/>.
        /// </summary>
        /// <param name="property">The child GProperty to update.</param>
        /// <param name="widget">The contained widget.</param>
        /// <param name="value">The new value of the property.</param>
        /// <typeparam name="T">The type of the <paramref name="value"/>.</typeparam>
        protected void SetChildProperty<T>(ChildProperty<T> property, Widget widget, T value)
        {
            ThrowIfDisposed();

            GObject.Sys.Value v = GObject.Sys.Value.From(value);
            Sys.Container.child_set_property(Handle, GetHandle(widget), property.Name, ref v);
        }

        /// <summary>
        /// Returns the value of the given child <paramref name="property"/> in the <paramref name="widget"/>.
        /// </summary>
        /// <param name="property">The child GProperty to fetch.</param>
        /// <param name="widget">The contained widget.</param>
        /// <typeparam name="T">The type of the value to return.</typeparam>
        protected T GetChildProperty<T>(ChildProperty<T> property, Widget widget)
        {
            ThrowIfDisposed();

            GObject.Sys.Value value = default;
            Sys.Container.child_get_property(Handle, GetHandle(widget), property.Name, ref value);

            if (TryWrapPointerAs<T>(value.To<IntPtr>(), out var ret))
                return ret;

            return value.To<T>();
        }

        #endregion
    }
}