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

        private void SetProperty(Value value, string? propertyName)
        {
            ThrowIfDisposed();

            if(propertyName is null)
                return;

            global::GObject.Object.set_property(handle, propertyName, ref value);
            value.Dispose();
        }

        protected void Set(IntPtr value, [CallerMemberName] string? propertyName = null) => SetProperty(value, propertyName);
        protected void SetEnum<T>(T e, [CallerMemberName] string? propertyName = null) where T : Enum => SetProperty((long)(object)e, propertyName);
        protected void Set(bool value, [CallerMemberName] string? propertyName = null) => SetProperty(value, propertyName);
        protected void Set(uint value, [CallerMemberName] string? propertyName = null) => SetProperty(value, propertyName);
        protected void Set(int value, [CallerMemberName] string? propertyName = null) => SetProperty(value, propertyName);
        protected void Set(string value, [CallerMemberName] string? propertyName = null) => SetProperty(value, propertyName);

        private Value GetProperty(string? propertyName)
        {
            ThrowIfDisposed();

            if(propertyName is null)
                return default;

            var value = new global::GObject.Value();
            global::GObject.Object.get_property(handle, propertyName, ref value);

            return value;
        }

        protected T GetEnum<T>([CallerMemberName] string? propertyName = null) where T:Enum
        {
            using var v = GetProperty(propertyName);
            return (T)((object)((long)v));
        }

        protected int GetInt([CallerMemberName] string? propertyName = null)
        {
            using var v = GetProperty(propertyName);
            return (int) v;
        }

        protected bool GetBool([CallerMemberName] string? propertyName = null)
        {
            using var v = GetProperty(propertyName);
            return (bool) v;
        }

        protected uint GetUInt([CallerMemberName] string? propertyName = null)
        {
            using var v = GetProperty(propertyName);
            return (uint) v;
        }

        protected string GetStr([CallerMemberName] string? propertyName = null)
        {
            using var v = GetProperty(propertyName);
            return (string) v;
        }

        protected IntPtr GetIntPtr([CallerMemberName] string? propertyName = null)
        {
            using var v = GetProperty(propertyName);
            return (IntPtr) v;
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

        public static implicit operator IntPtr (GObject val) => val.handle;

        public static implicit operator GObject (IntPtr val)
        {
            objects.TryGetValue(val, out var ret);
            return ret;
        }
    }
}