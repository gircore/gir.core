using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Reflection;

namespace GObject
{
    public partial class Object : IObject
    {
        protected static Sys.Type GetGType() => new Sys.Type(Sys.Object.get_type());
        private static readonly Dictionary<IntPtr, Object> objects = new Dictionary<IntPtr, Object>();

        private IntPtr handle;
        protected internal IntPtr Handle => handle;
        // private HashSet<Closure> closures = new HashSet<Closure>();
        private static readonly Dictionary<Closure, ulong> closures = new Dictionary<Closure, ulong>();

        // Constructs a new object
        public Object(params ConstructProp[] properties)
        {
            // This will automatically register our
            // type in the type dictionary. If the type is
            // a user-subclass, it will register it with
            // the GType type system automatically.
            var bla = GetType();
            var typeId = TypeDictionary.Get(bla);
            Console.WriteLine($"Instantiating {TypeDictionary.Get(typeId)}");

            // Pointer to GObject
            IntPtr handle;

            // Handle Properties
            int nProps = properties.Length;

            // TODO: Remove dual branches
            if (nProps > 0)
            {
                // We have properties
                // Prepare Construct Properties
                var names = new IntPtr[nProps];
                var values = new Sys.Value[nProps];

                // Populate arrays
                for (int i = 0; i < properties.Length; i++)
                {
                    var prop = properties[i];
                    // TODO: Marshal in a block, rather than one at a time
                    // for performance reasons.
                    names[i] = (IntPtr)Marshal.StringToHGlobalAnsi(prop.Name);
                    values[i] = prop.Value;
                }

                // Create with propeties
                handle = Sys.Object.new_with_properties(
                    typeId.Value,
                    (uint)names.Length,
                    ref names[0],
                    values
                );

                // Free strings
                foreach (var ptr in names)
                    Marshal.FreeHGlobal(ptr);
            }
            else
            {
                // Construct with no properties
                var zero = IntPtr.Zero;
                handle = Sys.Object.new_with_properties(
                    typeId.Value,
                    0,
                    ref zero,
                    Array.Empty<Sys.Value>()
                );
            }

            Initialize(handle);
        }

        // Initialises a wrapper for an existing object
        protected Object(IntPtr handle)
        {
            // TODO: Check to make sure the handle matches our
            // wrapper type.
            Initialize(handle);
        }

        private void Initialize(IntPtr ptr)
        {
            handle = ptr;
            objects.Add(ptr, this);
            RegisterProperties();
            RegisterOnFinalized();

            // Allow subclasses to perform initialisation
            Initialize();
        }

        // Wrappers can override here to perform
        // immediate initialisation
        protected virtual void Initialize() { }

        // TODO: Implement Virtual Methods
        // This will be done in a later PR
        protected virtual void Constructed() { }

        // Modify this in the future to play nicely with virtual function support?
        private void OnFinalized(IntPtr data, IntPtr where_the_object_was) => Dispose();
        private void RegisterOnFinalized() => Sys.Object.weak_ref(Handle, this.OnFinalized, IntPtr.Zero);

        // Property Notify Events
        protected internal void RegisterNotifyPropertyChangedEvent(string propertyName, Action callback)
            => RegisterEvent($"notify::{propertyName}", callback);

        protected internal void RegisterEvent(string eventName, ActionRefValues callback, bool after = false)
        {
            ThrowIfDisposed();
            RegisterEvent(eventName, new Closure(this, callback), after);
        }

        protected internal void RegisterEvent(string eventName, Action callback, bool after = false)
        {
            ThrowIfDisposed();
            RegisterEvent(eventName, new Closure(this, callback), after);
        }

        protected internal void UnregisterEvent(ActionRefValues callback)
        {
            ThrowIfDisposed();

            if (Closure.TryGetByDelegate(callback, out Closure closure))
                UnregisterEvent(closure);
        }

        protected internal void UnregisterEvent(Action callback)
        {
            ThrowIfDisposed();

            if (Closure.TryGetByDelegate(callback, out Closure closure))
                UnregisterEvent(closure);
        }

        private void RegisterProperties()
        {
            foreach (FieldInfo field in GetType().GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy))
            {
                if (field.FieldType.IsGenericType && field.FieldType.GetGenericTypeDefinition() == typeof(Property<>))
                {
                    var method = field.FieldType.GetMethod("RegisterNotifyEvent", BindingFlags.Instance | BindingFlags.NonPublic);
                    method?.Invoke(field.GetValue(this), new object[] { this });
                }
            }
        }

        private void RegisterEvent(string eventName, Closure closure, bool after)
        {
            if (closures.TryGetValue(closure, out ulong id) && Sys.Methods.signal_handler_is_connected(handle, id))
                return; // Skip if the handler is already registered

            var ret = Sys.Methods.signal_connect_closure(handle, eventName, closure, after);

            if (ret == 0)
                throw new Exception($"Could not connect to event {eventName}");

            // Add to our closures list so the callback
            // doesn't get garbage collected.
            // closures.Add(closure);
            closures[closure] = ret;
        }

        private void UnregisterEvent(Closure closure)
        {
            if (!closures.TryGetValue(closure, out ulong id))
                return;

            Sys.Methods.signal_handler_disconnect(handle, id);
            closures.Remove(closure);
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

        // This function returns the proxy object to the provided handle
        // if it already exists, otherwise creats a new wrapper object
        // and returns it.
        internal static bool TryWrapPointerAs<T>(IntPtr handle, out T o)
        {
            o = default!;

            // Return false if T is not of type Object
            if (!typeof(T).IsSubclassOf(typeof(Object)) && typeof(T) != typeof(Object))
                return false;

            // Attempt to lookup the pointer in the object dictionary
            if (objects.TryGetValue(handle, out var obj))
            {
                o = (T)(object)obj;
                return true;
            }

            // If it is not found, we can assume that it
            // is NOT a subclass type, as we ensure that
            // subclass types always outlive their pointers
            // TODO: Toggle Refs ^^^

            // Resolve gtype of object
            Sys.Type trueGType = TypeFromHandle(handle);
            var trueType = TypeDictionary.Get(trueGType);

            // Ensure we are not constructing a subclass
            if (IsSubclass(trueType))
                throw new Exception("Encountered foreign subclass pointer! This is a fatal error");

            // Ensure the conversion is valid
            var castGType = TypeDictionary.Get(typeof(T));
            if (!Sys.Methods.type_is_a(trueGType.Value, castGType.Value))
                throw new InvalidCastException();

            // Create using 'IntPtr' constructor
            o = (T)Activator.CreateInstance(
                trueType,
                obj.Handle
            );

            objects.Add(handle, (Object)(object)o);
            return true;
        }

        public static T WrapPointerAs<T>(IntPtr handle)
            where T : Object
        {
            if (TryWrapPointerAs(handle, out T obj))
                return obj;

            throw new Exception("Unable to wrap the given pointer as T");
        }

        protected static IntPtr GetHandle(Object obj)
            => obj.Handle;
    }
}