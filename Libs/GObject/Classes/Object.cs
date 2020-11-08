using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using GLib;

#nullable enable

namespace GObject
{
    public partial class Object : INotifyPropertyChanged, IDisposable
    {
        #region Fields

        private static readonly Dictionary<IntPtr, Object> Objects = new Dictionary<IntPtr, Object>();
        private static readonly Dictionary<ClosureHelper, ulong> Closures = new Dictionary<ClosureHelper, ulong>();

        #endregion

        #region Events

        /// <summary>
        /// Event triggered when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = null!;

        #endregion

        #region Properties

        protected internal IntPtr Handle { get; private set; }

        protected bool Disposed { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new object
        /// </summary>
        /// <param name="properties"></param>
        public Object(params ConstructParameter[] properties)
        {
            Type gtype = GetType().Register();

            // Handle Properties
            var nProps = properties.Length;
            
            var names = new IntPtr[nProps];
            var values = new Value[nProps]; //TODO: Do we need to dispose values?

            // Populate arrays
            for (var i = 0; i < nProps; i++)
            {
                ConstructParameter? prop = properties[i];
                // TODO: Marshal in a block, rather than one at a time
                // for performance reasons.
                names[i] = Marshal.StringToHGlobalAnsi(prop.Name);
                values[i] = prop.Value;
            }

            IntPtr namePointer = nProps > 0 ? names[0] : IntPtr.Zero;
            
            // Create with properties
            IntPtr handle = Native.new_with_properties(
                gtype.Value,
                (uint) names.Length,
                ref namePointer,
                values
            );

            // Free strings
            foreach (IntPtr ptr in names)
                Marshal.FreeHGlobal(ptr);

            Initialize(handle);
            
            Console.WriteLine($"Instantiating {GetType().FullName}");
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
        /// Wrappers can override here to perform immediate initialization.
        /// </summary>
        protected virtual void Initialize() { }

        // Modify this in the future to play nicely with virtual function support?
        private void OnFinalized(IntPtr data, IntPtr where_the_object_was) => Dispose();
        private void RegisterOnFinalized() => Native.weak_ref(Handle, OnFinalized, IntPtr.Zero);

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
                    System.Reflection.MethodInfo? method = field.FieldType.GetMethod(nameof(Property<Object>.RegisterNotifyEvent), MethodFlags);
                    method?.Invoke(field.GetValue(this), new object[] { this });
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

            // Add to our closures list so the callback
            // doesn't get garbage collected.
            // closures.Add(closure);
            Closures[closure] = ret;
        }

        protected internal void UnregisterEvent(ActionRefValues callback)
        {
            ThrowIfDisposed();

            if (ClosureHelper.TryGetByDelegate(callback, out ClosureHelper closure))
                UnregisterEvent(closure);
        }

        protected internal void UnregisterEvent(Action callback)
        {
            ThrowIfDisposed();

            if (ClosureHelper.TryGetByDelegate(callback, out ClosureHelper closure))
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

        protected static IntPtr GetHandle(Bytes bytes)
            => bytes.Handle;

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


        /// <summary>
        /// This function returns the proxy object to the provided handle
        /// if it already exists, otherwise creates a new wrapper object
        /// and returns it.
        /// </summary>
        /// <param name="handle"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>The wraped or object</returns>
        /// <exception cref="Exception">Exception is thrown if it pointer could not be wrapped.</exception>
        protected internal static T WrapPointerAs<T>(IntPtr handle)
            => (T) WrapPointer(handle);

        protected internal static bool TryWrapPointerAs<T>(IntPtr handle, out T obj)
        {
            try
            {
                obj = WrapPointerAs<T>(handle);
                return true;
            }
            catch
            {
                obj = default!;
                return false;
            }
        }

        protected internal static object WrapPointer(IntPtr handle)
        {
            // Attempt to lookup the pointer in the object dictionary
            if (Objects.TryGetValue(handle, out Object? obj))
                return obj;

            // Resolve GType of object
            Type gtype = handle.GetGTypeFromTypeInstance();

            // Return false if handle is not of type Object
            if (!Type.IsA(gtype, Types.Object))
                throw new Exception("Type is not an GObject");

            // If it is not found, we can assume that it
            // is NOT a subclass type, as we ensure that
            // subclass types always outlive their pointers
            // TODO: Toggle Refs ^^^
            
            System.Type? type = TypeDictionary.Get(gtype) ?? throw new Exception($"Could not find {gtype}");

            // Ensure we are not constructing a subclass
            if (type.IsSubclass())
                throw new Exception("Encountered foreign subclass pointer! This is a fatal error");
            
            // Create using 'IntPtr' constructor
            System.Reflection.ConstructorInfo ctor = type.GetConstructor(
                System.Reflection.BindingFlags.NonPublic
                | System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.Instance,
                null, new[] {typeof(IntPtr)}, null
            ) ?? throw new Exception($"constructor({nameof(IntPtr)}) not found for {type.FullName}");

            return ctor.Invoke(new object[] { handle });
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

                //TODO: Findout about closure release
                /*foreach(var closure in closures)
                    closure.Dispose();*/

                //TODO activate: closures.Clear();
            }
        }

        #endregion
    }
}
