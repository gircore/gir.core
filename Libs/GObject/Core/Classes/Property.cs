using System;

namespace GObject
{
    /// <summary>
    /// Describes a GProperty.
    /// </summary>
    /// <typeparam name="T">The type of the value this property will store.</typeparam>
    public sealed class Property<T>
    {
        #region Fields

        /// <summary>
        /// The property getter.
        /// </summary>
        private Func<Object, T>? _get;

        /// <summary>
        /// The property setter.
        /// </summary>
        private Action<Object, T>? _set;

        #endregion

        #region Properties

        /// <summary>
        /// The name of the GProperty.
        /// </summary>
        public string Name { get; private set; } = string.Empty;

        /// <summary>
        /// Checks if this GProperty is readable.
        /// </summary>
        public bool IsReadable => _get != null;

        /// <summary>
        /// Checks if this GProperty is writeable.
        /// </summary>
        public bool IsWriteable => _set != null;

        /// <summary>
        /// Checks if this GProperty a child property.
        /// </summary>
        public bool IsChild { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="Property{T}"/>.
        /// </summary>
        /// <remarks>
        /// This constructor is made private, since the end user need
        /// to register his properties using <see cref="Register{TObject}"/>
        /// or <see cref="RegisterChild{TObject}"/>.
        /// </remarks>
        private Property()
        { }

        #endregion

        #region Methods

        /// <summary>
        /// Registers this property descriptor and creates the correct GProperty
        /// into the GLib type of <typeparamref name="TObject"/>.
        /// </summary>
        /// <param name="name">The name of the GProperty to create.</param>
        /// <param name="get">The function called when retrieving the value of this property in bindings.</param>
        /// <param name="set">The function called when defing the value of this property in bindings.</param>
        /// <typeparam name="TObject">The type of the object on which this property will be registered.</typeparam>
        /// <returns>
        /// An instance of <see cref="Property{T}"/> representing the GProperty description.
        /// </returns>
        public static Property<T> Register<TObject>(string name, Func<TObject, T>? get = null, Action<TObject, T>? set = null)
            where TObject : Object
        {
            return RegisterInternal(name, false, get, set);
        }

        /// <summary>
        /// Registers this property descriptor and creates the correct child GProperty
        /// into the GLib type of <typeparamref name="TObject"/>.
        /// </summary>
        /// <param name="name">The name of the child GProperty to create.</param>
        /// <param name="get">The function called when retrieving the value of this child property in bindings.</param>
        /// <param name="set">The function called when defing the value of this child property in bindings.</param>
        /// <typeparam name="TObject">The type of the object on which this child property will be registered.</typeparam>
        /// <returns>
        /// An instance of <see cref="Property{T}"/> representing the child GProperty description.
        /// </returns>
        public static Property<T> RegisterChild<TObject>(string name, Func<TObject, T>? get = null, Action<TObject, T>? set = null)
            where TObject : Object
        {
            return RegisterInternal(name, true, get, set);
        }

        /// <summary>
        /// Get the value of this property in the given object.
        /// </summary>
        public T Get(Object o) => _get is null ? default! : _get(o);

        /// <summary>
        /// Set the value of this property in the given object
        /// using the given value.
        /// </summary>
        public void Set(Object o, T v) => _set?.Invoke(o, v);

        /// <summary>
        /// Registers this property descriptor and creates the correct GProperty
        /// or child GProperty into the GLib type of <typeparamref name="TObject"/>.
        /// </summary>
        /// <param name="name">The name of the GProperty to create.</param>
        /// <param name="isChild">Define if the GProperty is a child GProperty or not.</param>
        /// <param name="get">The function called when retrieving the value of this property in bindings.</param>
        /// <param name="set">The function called when defing the value of this property in bindings.</param>
        /// <typeparam name="TObject">The type of the object on which this property will be registered.</typeparam>
        /// <returns>
        /// An instance of <see cref="Property{T}"/> representing the GProperty description.
        /// </returns>
        private static Property<T> RegisterInternal<TObject>(string name, bool isChild, Func<TObject, T>? get = null, Action<TObject, T>? set = null)
            where TObject : Object
        {
            return new Property<T>
            {
                Name = name,
                IsChild = isChild,
                _get = get is null ? null : new Func<Object, T>((o) => get((TObject)o)),
                _set = set is null ? null : new Action<Object, T>((o, v) => set((TObject)o, v)),
            };
        }

        #endregion
    }
}