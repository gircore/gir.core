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
        public string Name { get; }

        /// <summary>
        /// The name of the C# property which serves as the
        /// proxy of this GProperty.
        /// </summary>
        public string PropertyName { get; }

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
        public bool IsChild { get; }

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
        /// <param name="name">The name of this GProperty</param>
        /// <param name="propertyName">The name of the C# property which serves as the proxy of this GProperty</param>
        /// <param name="isChild">Define if this GProperty is a child GProperty</param>
        private Property(string name, string propertyName, bool isChild)
        {
            Name = name;
            PropertyName = propertyName;
            IsChild = isChild;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Registers this property descriptor and creates the correct GProperty
        /// into the GLib type of <typeparamref name="TObject"/>.
        /// </summary>
        /// <param name="name">The name of the GProperty to create.</param>
        /// <param name="propertyName">The name of the C# property which serves as the proxy of this GProperty</param>
        /// <param name="get">The function called when retrieving the value of this property in bindings.</param>
        /// <param name="set">The function called when defing the value of this property in bindings.</param>
        /// <typeparam name="TObject">The type of the object on which this property will be registered.</typeparam>
        /// <returns>
        /// An instance of <see cref="Property{T}"/> representing the GProperty description.
        /// </returns>
        public static Property<T> Register<TObject>(string name, string propertyName, Func<TObject, T>? get = null, Action<TObject, T>? set = null)
            where TObject : Object
        {
            return RegisterInternal(name, propertyName, false, get, set);
        }

        /// <summary>
        /// Registers this property descriptor and creates the correct child GProperty
        /// into the GLib type of <typeparamref name="TObject"/>.
        /// </summary>
        /// <param name="name">The name of the child GProperty to create.</param>
        /// <param name="propertyName">The name of the C# property which serves as the proxy of this GProperty</param>
        /// <param name="get">The function called when retrieving the value of this child property in bindings.</param>
        /// <param name="set">The function called when defing the value of this child property in bindings.</param>
        /// <typeparam name="TObject">The type of the object on which this child property will be registered.</typeparam>
        /// <returns>
        /// An instance of <see cref="Property{T}"/> representing the child GProperty description.
        /// </returns>
        public static Property<T> RegisterChild<TObject>(string name, string propertyName, Func<TObject, T>? get = null, Action<TObject, T>? set = null)
            where TObject : Object
        {
            return RegisterInternal(name, propertyName, true, get, set);
        }

        /// <summary>
        /// Get the value of this property in the given object.
        /// </summary>
        public T Get(Object o) => _get is null
            ? throw new InvalidOperationException("Trying to read the value of a write-only property.")
            : _get(o);

        /// <summary>
        /// Set the value of this property in the given object
        /// using the given value.
        /// </summary>
        public void Set(Object o, T v)
            {
            if (_set is null)
                throw new InvalidOperationException("Trying to write the value of a read-only property.");

            _set(o, v);
        }

        /// <summary>
        /// Registers this GProperty into the given object.
        /// </summary>
        /// <param name="o">The object in which register the property.</param>
        internal void Register(Object o)
        {
            o.RegisterNotifyPropertyChangedEvent(Name, () => o.OnPropertyChanged(PropertyName));
        }

        #endregion
    }
}