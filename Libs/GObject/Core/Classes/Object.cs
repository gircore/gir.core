using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GLib.Core;

namespace GObject.Core
{
    public partial class GObject : Object
    {
        private static Dictionary<IntPtr, GObject> objects = new Dictionary<IntPtr, GObject>();

        private IntPtr handle;

        private HashSet<GClosure> closures;

//TODO: Register new Properties via a generic class? 
// private Property<string> MyProperty{get; set;}?
        protected GObject(IntPtr handle, bool isInitiallyUnowned = false)
        {
            objects.Add(handle, this);
            
            if(isInitiallyUnowned)
                this.handle = global::GObject.Object.ref_sink(handle);
            else
                this.handle = handle;

            closures = new HashSet<GClosure>();
            RegisterOnFinalized();
        }

        private void OnFinalized(IntPtr data, IntPtr where_the_object_was) => Dispose();
        private void RegisterOnFinalized() => global::GObject.Object.weak_ref(this, this.OnFinalized, IntPtr.Zero);

        internal protected void RegisterNotifyPropertyChangedEvent(string propertyName, Action callback) => RegisterEvent($"notify::{propertyName}", callback);

        internal protected void RegisterEvent(string eventName, ActionRefValues callback)
        {
            ThrowIfDisposed();
            RegisterEvent(eventName, new GClosure(this, callback));
        }

        internal protected void RegisterEvent(string eventName, Action callback)
        {
            ThrowIfDisposed();
            RegisterEvent(eventName, new GClosure(this, callback));
        }

        private void RegisterEvent(string eventName, GClosure closure)
        {
            var ret = global::GObject.Methods.signal_connect_closure(handle, eventName, closure, false);

            if(ret == 0)
                throw new Exception($"Could not connect to event {eventName}");

            closures.Add(closure);
        }

        protected T Convert<T>(IntPtr handle, Func<IntPtr, T> factory) where T : GObject?
        {
            var obj = (GObject?)handle;

            if(obj is null)
                return factory(handle);
            else
                return (T) obj;
        }

        private void ThrowIfDisposed()
        {
            if(Disposed)
                throw new Exception("Object is disposed");
        }

        protected void HandleError(IntPtr error)
        {
            if(error != IntPtr.Zero)
                throw new GException(error);
        }

        public static implicit operator IntPtr (GObject? val) => val?.handle ?? IntPtr.Zero;

        public static implicit operator GObject? (IntPtr val)
        {
            objects.TryGetValue(val, out var ret);
            return ret;
        }
    }
}