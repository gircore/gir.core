using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GObject
{
    public partial class Object : IObject
    {
        internal static Sys.Type GetGType() => new Sys.Type(Sys.Object.get_type());
        private static readonly Dictionary<IntPtr, Object> objects = new Dictionary<IntPtr, Object>();

        protected IntPtr handle;
        public IntPtr Handle => handle;
        private HashSet<Closure> closures = new HashSet<Closure>();

        // Constructs a new object
        public Object(params ConstructProp[] properties)
        {
            // This will automatically register our
            // type in the type dictionary. If the type is
            // a user-subclass, it will register it with
            // the GType type system automatically.
            var typeId = TypeDictionary.Get(GetType());
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
                    typeId, 
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
                    typeId, 
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
            RegisterOnFinalized();

            // Allow subclasses to perform initialisation
            Initialize();
        }

        // Wrappers can override here to perform
        // immediate initialisation
        protected virtual void Initialize() {}

        // TODO: Implement Virtual Methods
        // This will be done in a later PR
        protected virtual void Constructed() {}
        
        // Modify this in the future to play nicely with virtual function support?
        private void OnFinalized(IntPtr data, IntPtr where_the_object_was) => Dispose();
        private void RegisterOnFinalized() => Sys.Object.weak_ref(Handle, this.OnFinalized, IntPtr.Zero);

        // Property Notify Events
        internal protected void RegisterNotifyPropertyChangedEvent(string propertyName, Action callback) 
            => RegisterEvent($"notify::{propertyName}", callback);

        // Signal Handling
        internal protected void RegisterEvent(string eventName, ActionRefValues callback)
        {
            ThrowIfDisposed();
            RegisterEvent(eventName, new Closure(this, callback));
        }

        internal protected void RegisterEvent(string eventName, Action callback)
        {
            ThrowIfDisposed();
            RegisterEvent(eventName, new Closure(this, callback));
        }

        private void RegisterEvent(string eventName, Closure closure)
        {
            var ret = Sys.Methods.signal_connect_closure(handle, eventName, closure, false);

            if(ret == 0)
                throw new Exception($"Could not connect to event {eventName}");

            // Add to our closures list so the callback
            // doesn't get garbage collected.
            closures.Add(closure);
        }

        private void ThrowIfDisposed()
        {
            if(Disposed)
                throw new Exception("Object is disposed");
        }

        protected static void HandleError(IntPtr error)
        {
            if(error != IntPtr.Zero)
                throw new GLib.GException(error);
        }

        // This function returns the proxy object to the provided handle
        // if it already exists, otherwise creats a new wrapper object
        // and returns it.
        public static T WrapPointerAs<T>(IntPtr handle)
            where T: Object
        {
            // Attempt to lookup the pointer in the object dictionary
            if (objects.TryGetValue(handle, out var obj))
                return (T)obj;

            // If it is not found, we can assume that it
            // is NOT a subclass type, as we ensure that
            // subclass types always outlive their pointers
            // TODO: Toggle Refs ^^^

            // Resolve gtype of object
            Sys.Type trueGType = TypeFromHandle(handle);
            Type trueType = TypeDictionary.Get(trueGType);

            // Ensure we are not constructing a subclass
            if (IsSubclass(trueType))
                throw new Exception("Encountered foreign subclass pointer! This is a fatal error");

            // Ensure the conversion is valid
            Sys.Type castGType = TypeDictionary.Get(typeof(T));
            if (!Sys.Methods.type_is_a(trueGType, castGType))
                throw new InvalidCastException();

            // Create using 'IntPtr' constructor
            return (T)Activator.CreateInstance(
                trueType,
                new object[] { obj.Handle }
            );
        }
    }
}