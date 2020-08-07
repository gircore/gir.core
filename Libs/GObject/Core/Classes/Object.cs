using System;
using System.Collections.Generic;

namespace GObject
{
    public partial class Object : IObject
    {
        private static readonly Dictionary<IntPtr, Object> objects = new Dictionary<IntPtr, Object>();

        private IntPtr _handle;
        public IntPtr Handle => _handle;

        private HashSet<Closure> closures;

        protected Object(IntPtr handle, bool isInitiallyUnowned = false)
        {
            objects.Add(handle, this);
            
            if(isInitiallyUnowned)
                this._handle = Sys.Object.ref_sink(handle);
            else
                this._handle = handle;

            closures = new HashSet<Closure>();
            RegisterOnFinalized();
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
            var ret = Sys.Methods.signal_connect_closure(_handle, eventName, closure, false);

            if(ret == 0)
                throw new Exception($"Could not connect to event {eventName}");

            closures.Add(closure);
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

        public static bool TryGetObject<T>(IntPtr handle, out T obj) where T: Object
        { 
            var result = objects.TryGetValue(handle, out var ret);
            obj = (T) ret;
            
            return result;
        }
    }
}