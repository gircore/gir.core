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
        private Func<IObject, T>? _get;

        /// <summary>
        /// The property setter.
        /// </summary>
        private Action<IObject, T>? _set;

        /// <summary>
        /// The type getter
        /// </summary>
        private Func<Type>? _getType;

        /// <summary>
        /// Property kind.
        /// </summary>
        private Types _kind;
        
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

        public Types Kind => _kind;
        
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="Property{T}"/>.
        /// </summary>
        /// <remarks>
        /// This constructor is made private, since the end user need
        /// to register his properties using <see cref="Register{TObject}"/>.
        /// </remarks>
        /// <param name="name">The name of this GProperty</param>
        /// <param name="propertyName">The name of the C# property which serves as the proxy of this GProperty.</param>
        private Property(string name, string propertyName)
        {
            Name = name;
            PropertyName = propertyName;
        }

        #endregion

        #region Methods

        public Type? GetGType() => _getType?.Invoke();
        
        /// <summary>
        /// Registers this property descriptor and creates the correct GProperty
        /// into the GLib type of <typeparamref name="TObject"/>.
        /// </summary>
        /// <param name="name">The name of the GProperty to create.</param>
        /// <param name="propertyName">The name of the C# property which serves as the proxy of this GProperty</param>
        /// <param name="get">The function called when retrieving the value of this property in bindings.</param>
        /// <param name="set">The function called when defining the value of this property in bindings.</param>
        /// <param name="getType">The function called to get the type of this property</param>
        /// <param name="kind">The kind of this property</param>
        /// <typeparam name="TObject">The type of the object on which this property will be registered.</typeparam>
        /// <returns>
        /// An instance of <see cref="Property{T}"/> representing the GProperty description.
        /// </returns>
        public static Property<T> Register<TObject>(string name, string propertyName, Func<TObject, T>? get = null, Action<TObject, T>? set = null, Func<Type>? getType = null, Types kind = Types.Invalid)
            where TObject : Object
        {
            return new Property<T>(name, propertyName)
            {
                _get = get is null ? null : new Func<IObject, T>((o) => get((TObject) o)),
                _set = set is null ? null : new Action<IObject, T>((o, v) => set((TObject) o, v)),
                _getType = getType,
                _kind = kind
            };
        }

        /// <summary>
        /// Wrap this property descriptor on an existing GProperty
        /// from a GLib type of <typeparamref name="TObject"/>.
        /// </summary>
        /// <param name="name">The name of the GProperty to wrap.</param>
        /// <param name="propertyName">The name of the C# property which serves as the proxy of this GProperty</param>
        /// <param name="get">The function called when retrieving the value of this property in bindings.</param>
        /// <param name="set">The function called when defining the value of this property in bindings.</param>
        /// <param name="getType">The function called to get the type of this property</param>
        /// <param name="kind">The kind of this property</param>
        /// <typeparam name="TObject">The type of the object on which this property will be wrapped.</typeparam>
        /// <returns>
        /// An instance of <see cref="Property{T}"/> representing the GProperty description.
        /// </returns>
        public static Property<T> Wrap<TObject>(string name, string propertyName, Func<TObject, T>? get = null, Action<TObject, T>? set = null, Func<Type>? getType = null, Types kind = Types.Invalid)
            where TObject : IObject
        {
            return new Property<T>(name, propertyName)
            {
                _get = get is null ? null : new Func<IObject, T>((o) => get((TObject) o)),
                _set = set is null ? null : new Action<IObject, T>((o, v) => set((TObject) o, v)),
                _getType = getType,
                _kind = kind
            };
        }

        /// <summary>
        /// Get the value of this property in the given object.
        /// </summary>
        public T Get(IObject o) => _get is null
            ? throw new InvalidOperationException("Trying to read the value of a write-only property.")
            : _get(o);

        /// <summary>
        /// Set the value of this property in the given object
        /// using the given value.
        /// </summary>
        public void Set(IObject o, T v)
        {
            if (_set is null)
                throw new InvalidOperationException("Trying to write the value of a read-only property.");

            _set(o, v);
        }

        /// <summary>
        /// Registers this GProperty for notify events into the given object.
        /// </summary>
        /// <param name="o">The object in which register the property.</param>
        internal void RegisterNotifyEvent(Object o)
        {
            o.RegisterNotifyPropertyChangedEvent(Name, () => o.OnPropertyChanged(PropertyName));
        }

        #endregion
    }
}
