using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GObject
{
    // Stub Class
    // Floating references handled by the base GObject class
    [Wrapper("GInitiallyUnowned")]
    public class InitiallyUnowned : Object
    {
        protected InitiallyUnowned() {}
    }

    // GObject Wrapper
    [Wrapper("GObject")]
    public partial class Object : IObject
    {
        // Object Dictionary
        private static readonly Dictionary<IntPtr, Object> objects = new Dictionary<IntPtr, Object>();

        // Pointer to underlying GObject
        private IntPtr _handle;
        public IntPtr Handle => _handle;

        // Member Fields
        private HashSet<Closure> closures;        

        // Wrapper vs Subclass Helper Function
        // Uses presence of [Wrapper(type)] attribute
        private static bool IsSubclass(Type type)
            => type != typeof(Object) &&
               type != typeof(InitiallyUnowned) &&
               !Attribute.IsDefined(type, typeof(WrapperAttribute));

        // New GObject Constructor
        public Object()
        {
            var zero = new IntPtr(0);
            IntPtr handle = Sys.Object.new_with_properties(
                TypeDictionary.FromType(GetType()),
                0, ref zero, new Sys.Value[0]
            );
            
            Initialize(out closures, handle);
        }

        // Wrap GObject Constructor
        protected Object(IntPtr handle, bool isInitiallyUnowned = false)
            => Initialize(out closures, handle, isInitiallyUnowned);

        // Common Initialisation Code
        private void Initialize(out HashSet<Closure> closures, IntPtr handle, bool isInitiallyUnowned = false)
        {
            // Register on first run
            var type = this.GetType();
            if (!TypeDictionary.Contains(type))
                RegisterType(type);

            // Add to object dictionary
            objects[handle] = this;
            
            // If floating reference, sink immediately
            if(isInitiallyUnowned)
                this._handle = Sys.Object.ref_sink(handle);
            else
                this._handle = handle;

            closures = new HashSet<Closure>();
            RegisterOnFinalized();
        }

        // Registers the object in the Type Dictionary
        internal static void RegisterType(Type type)
        {
            if (IsSubclass(type))
                RegisterClass(type);
            else
            {
                // Lookup by Type Name
                var attr = (WrapperAttribute)Attribute.GetCustomAttribute(type, typeof(WrapperAttribute));
                string typeName = attr.TypeName;
                ulong typeid = Sys.Methods.type_from_name(typeName);

                // TODO: Work on error handling/backup methods
                if (typeid == 0)
                    throw new Exception("Type could not be found. This is a fatal error!");
                    
                TypeDictionary.RegisterType(type, typeid);
            }
        }

        // Print class name
        public override string ToString()
        {
            IntPtr ptr = Sys.Methods.type_name_from_instance(Handle);
            string? name = Marshal.PtrToStringAnsi(ptr);
            return name ?? throw new Exception("Could not lookup type name");
        }

        private void OnFinalized(IntPtr data, IntPtr where_the_object_was) => Dispose();
        private void RegisterOnFinalized() => Sys.Object.weak_ref(Handle, this.OnFinalized, IntPtr.Zero);

        internal protected void RegisterNotifyPropertyChangedEvent(string propertyName, Action callback) 
            => RegisterEvent($"notify::{propertyName}", callback);

        // Signal Registration
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
            var ret = Sys.Methods.signal_connect_closure(_handle, eventName, closure.Handle, false);

            if(ret == 0)
                throw new Exception($"Could not connect to event {eventName}");

            closures.Add(closure);
        }

        // Wraps a pointer in the corresponding C# type, before returning it
        // in the form type T. Note, T is the type returned, not the actual
        // type of the object.
        public static T WrapPointer<T>(IntPtr handle, Func<IntPtr, T>? factory = null, bool owned = true)
            where T : Object
        {
            // Get System.Type from GType
            // TODO: FIND ACTUAL TYPE
            Type type = typeof(T);

            // If ptr is in object dict, then return
            if (objects.TryGetValue(handle, out Object obj))
                return (T)obj;

            // By design, all user-defined subclass objects will outlive their
            // custom GObject representation. Therefore, if we do not find the
            // object in the dictionary, this is a fatal error.
            if (IsSubclass(type))
                throw new NotImplementedException("Type garbage collected. Implement ToggleRefs!");

            // Instantiate a new wrapper type to wrap our ptr
            return factory?.Invoke(handle) ?? throw new Exception("Handle null reference!");
        }

        /*public static T Convert<T>(IntPtr handle, Func<IntPtr, T> factory) where T : Object
        {
            if(TryGetObject(handle, out T obj))
                return obj;
            else
                return factory(handle);
        }*/

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

        public virtual Sys.Type GetGObjectType()
        {
            //TODO: Return value is not wrapped
            return new Sys.Type(Sys.Object.get_type());
        }

        /*public static bool TryGetObject<T>(IntPtr handle, out T obj) where T: Object
        { 
            var result = objects.TryGetValue(handle, out var ret);
            obj = (T) ret;
            
            return result;
        }*/
    }
}