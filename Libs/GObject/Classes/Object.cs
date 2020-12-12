using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GObject
{
    public partial class Object : IObject, INotifyPropertyChanged, IDisposable
    {
        #region Fields

        private static readonly Dictionary<IntPtr, Object> Objects = new Dictionary<IntPtr, Object>();
        private static readonly Dictionary<ClosureHelper, ulong> Closures = new Dictionary<ClosureHelper, ulong>();

        #endregion

        #region Events

        /// <summary>
        /// Event triggered when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion

        #region Properties
        
        protected internal IntPtr Handle { get; private set; }
        
        // We need to store a reference to WeakNotify to
        // prevent the delegate from being collected by the GC
        private WeakNotify? _onFinalized;

        private bool Disposed { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new object
        /// </summary>
        /// <param name="properties"></param>
        public Object(params ConstructParameter[] properties)
        {
            // This will automatically register our
            // type in the type dictionary. If the type is
            // a user-subclass, it will register it with
            // the GType type system automatically.
            System.Type? t = GetType();
            Type typeId = TypeDictionary.Get(t);
            Console.WriteLine($"Instantiating {TypeDictionary.Get(typeId)}");

            // Pointer to GObject
            IntPtr handle;

            // Handle Properties
            var nProps = properties.Length;

            // TODO: Remove dual branches
            if (nProps > 0)
            {
                // We have properties
                // Prepare Construct Properties
                var names = new IntPtr[nProps];
                var values = new Value[nProps];

                // Populate arrays
                for (var i = 0; i < properties.Length; i++)
                {
                    ConstructParameter? prop = properties[i];
                    // TODO: Marshal in a block, rather than one at a time
                    // for performance reasons.
                    names[i] = Marshal.StringToHGlobalAnsi(prop.Name);
                    values[i] = prop.Value;
                }

                // Create with properties
                handle = Native.new_with_properties(
                    typeId.Value,
                    (uint) names.Length,
                    ref names[0],
                    values
                );

                // Free strings
                foreach (IntPtr ptr in names)
                    Marshal.FreeHGlobal(ptr);
            }
            else
            {
                // Construct with no properties
                IntPtr zero = IntPtr.Zero;
                handle = Native.new_with_properties(
                    typeId.Value,
                    0,
                    ref zero,
                    Array.Empty<Value>()
                );
            }

            Initialize(handle);
        }

        /// <summary>
        /// Initializes a wrapper for an existing object
        /// </summary>
        /// <param name="handle"></param>
        protected Object(IntPtr handle)
        {
            // TODO: Check to make sure the handle matches our
            // wrapper type.
            Initialize(handle);
        }

        ~Object() => Dispose(false);

        #endregion

        #region Methods

        private void Initialize(IntPtr ptr)
        {
            Handle = ptr;
            //TODO
            Objects.Add(ptr, this);
            RegisterProperties();
            RegisterOnFinalized();

            // Allow subclasses to perform initialization
            Initialize();
        }

        /// <summary>
        ///  Wrappers can override here to perform immediate initialization
        /// </summary>
        protected virtual void Initialize() { }

        // Modify this in the future to play nicely with virtual function support?
        private void OnFinalized(IntPtr data, IntPtr where_the_object_was) => Dispose();
        private void RegisterOnFinalized()
        {
            _onFinalized = OnFinalized;
            Native.weak_ref(Handle, _onFinalized, IntPtr.Zero);
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
                    method?.Invoke(field.GetValue(this), new object[] {this});
                }
            }
        }

        // Property Notify Events
        protected internal void RegisterNotifyPropertyChangedEvent(string propertyName, Action callback)
            => RegisterEvent($"notify::{propertyName}", callback);

        protected internal void RegisterEvent(string eventName, ActionRefValues callback, bool after = false)
        {
            ThrowIfDisposed();
            RegisterEvent(eventName, new ClosureHelper(this, callback), after);
        }

        protected internal void RegisterEvent(string eventName, Action callback, bool after = false)
        {
            ThrowIfDisposed();
            RegisterEvent(eventName, new ClosureHelper(this, callback), after);
        }

        private void RegisterEvent(string eventName, ClosureHelper closure, bool after)
        {
            if (Closures.TryGetValue(closure, out var id) && Global.Native.signal_handler_is_connected(Handle, id))
                return; // Skip if the handler is already registered

            var ret = Global.Native.signal_connect_closure(Handle, eventName, closure.Handle, after);

            if (ret == 0)
                throw new Exception($"Could not connect to event {eventName}");

            // Add to our closures list so the callback doesn't get garbage collected.
            Closures[closure] = ret;
        }

        protected internal void UnregisterEvent(ActionRefValues callback)
        {
            ThrowIfDisposed();

            if (ClosureHelper.TryGetByDelegate(callback, out ClosureHelper? closure))
                UnregisterEvent(closure);
        }

        protected internal void UnregisterEvent(Action callback)
        {
            ThrowIfDisposed();

            if (ClosureHelper.TryGetByDelegate(callback, out ClosureHelper? closure))
                UnregisterEvent(closure);
        }

        private void UnregisterEvent(ClosureHelper closure)
        {
            if (!Closures.TryGetValue(closure, out var id))
                return;

            Global.Native.signal_handler_disconnect(Handle, id);
            Closures.Remove(closure);
        }

        protected void ThrowIfDisposed()
        {
            if (Disposed)
                throw new Exception("Object is disposed");
        }

        protected static void HandleError(IntPtr error)
        {
            if (error != IntPtr.Zero)
                throw new GLib.GException(error);
        }

        protected static IntPtr GetHandle(Object obj)
            => obj.Handle;

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

        protected internal static bool GetObject<T>(IntPtr handle, out T obj) where T : Object
        {
            if (Objects.TryGetValue(handle, out Object? foundObj))
            {
                obj = (T) foundObj;
                return true;
            }

            obj = default!;
            return false;
        }

        // This function returns the proxy object to the provided handle
        // if it already exists, otherwise creates a new wrapper object
        // and returns it.
        protected static T WrapPointerAs<T>(IntPtr handle)
            where T : Object
        {
            if (TryWrapPointerAs<T>(handle, out T obj))
                return obj;

            throw new Exception($"Failed to wrap handle as type <{typeof(T).FullName}>");
        }

        protected internal static bool TryWrapPointerAs<T>(IntPtr handle, out T o)
            where T : Object
        {
            o = default!;

            // Return false if T is not of type Object
            // TODO: Remove this?
            if (!typeof(T).IsSubclassOf(typeof(Object)) && typeof(T) != typeof(Object))
                return false;

            // Attempt to lookup the pointer in the object dictionary
            if (Objects.TryGetValue(handle, out Object? obj))
            {
                o = (T) (object) obj;
                return true;
            }

            // If it is not found, we can assume that it is NOT a subclass type,
            // as we ensure that subclass types always outlive their pointers
            // TODO: Toggle Refs ^^^

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

            // Ensure we are not constructing a subclass
            // TODO: This can be removed once ToggleRefs are implemented
            if (IsSubclass(trueType))
                throw new Exception("Encountered foreign subclass pointer! This is a fatal error");

            // Create using 'IntPtr' constructor
            System.Reflection.ConstructorInfo? ctor = trueType.GetConstructor(
                System.Reflection.BindingFlags.NonPublic
                | System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.Instance,
                null, new[] {typeof(IntPtr)}, null
            );
            
            if (ctor == null)
                throw new Exception($"Type {trueType.FullName} does not define an IntPtr constructor. This could mean improperly defined bindings");

            o = (T) ctor.Invoke(new object[] {handle});

            return true;
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
            if (!Disposed)
            {
                Disposed = true;

                if (Handle != IntPtr.Zero)
                {
                    Native.unref(Handle);
                    Objects.Remove(Handle);
                }

                Handle = IntPtr.Zero;

                // TODO: Find out about closure release
                /*foreach(var closure in closures)
                    closure.Dispose();*/

                // TODO activate: closures.Clear();
            }
        }

        #endregion
    }
}
