using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using GLib;

namespace GObject
{
    public partial class Object : IObject, INotifyPropertyChanged, IDisposable, IHandle
    {
        #region Fields

        private static readonly Dictionary<IntPtr, ToggleRef<Object>> SubclassObjects = new();
        private static readonly Dictionary<IntPtr, WeakReference<Object>> WrapperObjects = new();

        private readonly Dictionary<string, SignalHelper> _signals = new();

        #endregion

        #region Events

        /// <summary>
        /// Event triggered when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion

        #region Properties

        public IntPtr Handle { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new object
        /// </summary>
        /// <param name="properties"></param>
        /// <remarks>This constructor is protected to be sure that there is no caller (enduser) keeping a reference to
        /// the construct parameters as the contained values are freed at the end of this constructor.
        /// If certain constructors are needed they need to be implemented with concrete constructor arguments in
        /// a higher layer.</remarks>
        protected Object(params ConstructParameter[] properties)
        {
            // This will automatically register our
            // type in the type dictionary. If the type is
            // a user-subclass, it will register it with
            // the GType type system automatically.
            System.Type? t = GetType();
            Type typeId = TypeDictionary.Get(t);
            Console.WriteLine($"Instantiating {TypeDictionary.Get(typeId)}");

            var names = new IntPtr[properties.Length];
            var values = new Value[properties.Length];

            IntPtr handle;

            try
            {
                for (var i = 0; i < properties.Length; i++)
                {
                    ConstructParameter? prop = properties[i];
                    // TODO: Marshal in a block, rather than one at a time
                    // for performance reasons.
                    names[i] = Marshal.StringToHGlobalAnsi(prop.Name);
                    values[i] = prop.Value;
                }

                IntPtr zero = IntPtr.Zero;

                handle = Native.new_with_properties(
                    typeId.Value,
                    (uint) properties.Length,
                    ref (properties.Length > 0 ? ref names[0] : ref zero),
                    values
                );

                if (Native.is_floating(handle))
                    Native.ref_sink(handle); //Make sure handle is owned by us
            }
            finally
            {
                foreach (IntPtr ptr in names)
                    Marshal.FreeHGlobal(ptr);

                foreach (Value value in values)
                    value.Dispose();
            }

            Initialize(handle);
        }

        /// <summary>
        /// Initializes a wrapper for an existing object
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="ownedRef">Defines if the handle is owned by us. If not owned by us it is refed to keep it around.</param>
        protected Object(IntPtr handle, bool ownedRef)
        {
            if (!ownedRef)
            {
                // - Unowned GObjects need to be refed to bind them to this instance
                // - Unowned InitiallyUnowned floating objects need to be ref_sinked
                // - Unowned InitiallyUnowned non-floating objects need to be refed
                // As ref_sink behaves like ref in case of non floating instances we use it for all 3 cases
                Native.ref_sink(handle);
            }
            else
            {
                //In case we own the ref because the ownership was fully transfered to us we
                //do not need to ref the object at all.

                Debug.Assert(!Native.is_floating(handle), "Owned floating references are not possible.");
            }

            Initialize(handle);
        }

        ~Object()
        {
            Dispose(false);
        }

        #endregion

        #region Methods

        private void Initialize(IntPtr ptr)
        {
            Handle = ptr;

            RegisterObject();
            RegisterProperties();

            Initialize();
        }

        /// <summary>
        /// Wrapper and subclasses can override here to perform immediate initialization
        /// </summary>
        protected virtual void Initialize() { }

        private void RegisterObject()
        {
            if (IsSubclass(GetType()))
            {
                lock (SubclassObjects)
                {
                    SubclassObjects[Handle] = new ToggleRef<Object>(this);
                }
            }
            else
            {
                lock (WrapperObjects)
                {
                    WrapperObjects[Handle] = new WeakReference<Object>(this);
                }
            }
        }

        private void RegisterProperties()
        {
            const System.Reflection.BindingFlags PropertyDescriptorFieldFlags = System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.Static
                | System.Reflection.BindingFlags.FlattenHierarchy;

            const System.Reflection.BindingFlags MethodFlags = System.Reflection.BindingFlags.Instance
                | System.Reflection.BindingFlags.NonPublic;

            foreach (System.Reflection.FieldInfo? field in GetType().GetFields(PropertyDescriptorFieldFlags))
            {
                if (field.FieldType.IsGenericType && field.FieldType.GetGenericTypeDefinition() == typeof(Property<>))
                {
                    System.Reflection.MethodInfo? method =
                        field.FieldType.GetMethod(nameof(Property<Object>.RegisterNotifyEvent), MethodFlags);
                    method?.Invoke(field.GetValue(this), new object[] { this });
                }
            }
        }

        protected internal SignalHelper GetSignalHelper(string name)
        {
            if (_signals.TryGetValue(name, out var signalHelper))
                return signalHelper;

            signalHelper = new SignalHelper(this, name);
            _signals.Add(name, signalHelper);

            return signalHelper;
        }

        /// <summary>
        /// Notify this object that a property has just changed.
        /// </summary>
        /// <param name="propertyName">The name of the property who changed.</param>
        protected internal void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            if (propertyName == null)
                return;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static T? WrapNullableHandle<T>(IntPtr handle, bool ownedRef) where T : Object
        {
            if (handle == IntPtr.Zero)
                return null;

            return WrapHandle<T>(handle, ownedRef);
        }

        /// <summary>
        /// This function returns the proxy object to the provided handle
        /// if it already exists, otherwise creates a new wrapper object
        /// and returns it. Note that <typeparamref name="T"/> is the type
        /// the object should be returned. It is independent of the object's
        /// actual type and is provided purely for convenience.
        /// </summary>
        /// <param name="handle">A pointer to the native GObject that should be wrapped.</param>
        /// <param name="ownedRef">Specify if the ref is owned by us, because ownership was transferred.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>A C# proxy object which wraps the native GObject.</returns>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="Exception"></exception>
        public static T WrapHandle<T>(IntPtr handle, bool ownedRef) where T : Object
        {
            if (handle == IntPtr.Zero)
                throw new NullReferenceException(
                    $"Failed to wrap handle as type <{typeof(T).FullName}>. Null handle passed to WrapHandle.");

            if (TryGetObject(handle, out T? obj))
                return obj;

            // Resolve GType of object
            Type trueGType = TypeFromHandle(handle);
            System.Type? trueType = null;

            // Ensure 'T' is registered in type dictionary for future use. It is an error for a
            // wrapper type to not define a TypeDescriptor. 
            TypeDescriptor desc = TypeDescriptorRegistry.ResolveTypeDescriptorForType(typeof(T));

            TypeDictionary.AddRecursive(typeof(T), desc.GType);

            // Optimisation: Compare the gtype of 'T' to the GType of the pointer. If they are
            // equal, we can skip the type dictionary's (possible) recursive lookup and return
            // immediately.
            if (desc.GType.Equals(trueGType))
            {
                // We are actually a type 'T'.
                // The conversion will always be valid
                trueType = typeof(T);
            }
            else
            {
                // We are some other representation of 'T' (e.g. a more derived type)
                // Retrieve the normal way
                trueType = TypeDictionary.Get(trueGType);

                // Ensure the conversion is valid
                Type castGType = TypeDictionary.Get(typeof(T));
                if (!Global.Native.type_is_a(trueGType.Value, castGType.Value))
                    throw new InvalidCastException();
            }

            // Create using 'IntPtr' constructor
            System.Reflection.ConstructorInfo? ctor = trueType.GetConstructor(
                System.Reflection.BindingFlags.NonPublic
                | System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.Instance,
                null, new[] { typeof(IntPtr), typeof(bool) }, null
            );

            if (ctor == null)
                throw new Exception($"Type {trueType.FullName} does not define an IntPtr constructor. This could mean improperly defined bindings");

            return (T) ctor.Invoke(new object[] { handle, ownedRef });
        }

        private static bool TryGetObject<T>(IntPtr handle, [NotNullWhen(true)] out T? obj) where T : Object
        {
            if (WrapperObjects.TryGetValue(handle, out WeakReference<Object>? weakRef))
            {
                if (weakRef.TryGetTarget(out Object? weakObj))
                {
                    obj = (T) weakObj;
                    return true;
                }
            }
            else if (SubclassObjects.TryGetValue(handle, out ToggleRef<Object>? toggleObj))
            {
                if (toggleObj.Object is not null)
                {
                    obj = (T) toggleObj.Object;
                    return true;
                }
            }

            obj = null;
            return false;
        }

        /// <summary>
        /// A variant of <see cref="WrapHandle{T}"/> which fails gracefully if the pointer cannot be wrapped.
        /// </summary>
        /// <param name="handle">A pointer to the native GObject that should be wrapped.</param>
        /// <param name="o">A C# proxy object which wraps the native GObject.</param>
        /// <param name="ownedRef">Specify if the ref is owned by us, because ownership was transferred.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns><c>true</c> if the handle was wrapped, or <c>false</c> if something went wrong.</returns>
        public static bool TryWrapHandle<T>(IntPtr handle, bool ownedRef, [NotNullWhen(true)] out T? o)
            where T : Object
        {
            o = null;
            try
            {
                o = WrapHandle<T>(handle, ownedRef);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Could not wrap handle as type {typeof(T).FullName}: {e.Message}");
                return false;
            }
        }

        #endregion

        #region IDisposable Implementation

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (Handle == IntPtr.Zero)
                return;

            if (disposing)
            {
                foreach (var signalHelper in _signals.Values)
                    signalHelper.Dispose();
            }

            lock (WrapperObjects)
            {
                WrapperObjects.Remove(Handle);
            }

            lock (SubclassObjects)
            {
                SubclassObjects.Remove(Handle);
            }

            Native.unref(Handle);
            Handle = IntPtr.Zero;
        }

        #endregion
    }
}
