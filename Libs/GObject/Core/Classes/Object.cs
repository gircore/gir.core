using System;
using System.Collections.Generic;

namespace GObject
{
    public partial class Object : IObject
    {
        internal static Sys.Type GetGType() => new Sys.Type(Sys.Object.get_type());
        private static readonly Dictionary<IntPtr, Object> objects = new Dictionary<IntPtr, Object>();

        protected IntPtr handle;
        internal IntPtr Handle => handle;
        private HashSet<Closure> closures = new HashSet<Closure>();

        public Object(params Prop[] properties)
        {
            RegisterType(GetType());
            //TODO: use properties
            var zero = IntPtr.Zero;
            var typeId = GetGTypeFor(GetType());
            var ptr = Sys.Object.new_with_properties(
                typeId, 
                0, 
                ref zero, 
                new Sys.Value[0]
            );
            
            Initialize(ptr);
        }
        
        protected Object(IntPtr handle)
        {
            Initialize(handle);
        }

        private void Initialize(IntPtr ptr)
        {
            handle = ptr;
            objects.Add(ptr, this);
            RegisterOnFinalized();
        }
        
        protected Object(IntPtr handle, bool isInitiallyUnowned = false)
        {
            //TODO : Obsolete
        }

        protected virtual void Constructed()
        {
            
        }
        
        private void OnFinalized(IntPtr data, IntPtr where_the_object_was) => Dispose();
        private void RegisterOnFinalized() => Sys.Object.weak_ref(this, this.OnFinalized, IntPtr.Zero);

        internal protected void RegisterNotifyPropertyChangedEvent(string propertyName, Action callback) 
            => RegisterEvent($"notify::{propertyName}", callback);

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

            //TODO activate: closures.Add(closure);
        }

        public static T Convert<T>(IntPtr handle, Func<IntPtr, T> factory) where T : Object
        {
            if(TryGetObject(handle, out T obj))
                return obj;
            else
                return factory(handle);
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

        public static implicit operator IntPtr (Object? val) => val?.handle ?? IntPtr.Zero;

        //TODO: Remove implicit cast
        public static implicit operator Object? (IntPtr val)
        {
            objects.TryGetValue(val, out var ret);
            return ret;
        }

        public static bool TryGetObject<T>(IntPtr handle, out T obj) where T: Object
        { 
            var result = objects.TryGetValue(handle, out var ret);
            obj = (T) ret;
            
            return result;
        }
    }
}