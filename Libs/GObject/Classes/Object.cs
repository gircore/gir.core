using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GObject
{
    public partial class Object : INotifyPropertyChanged, IDisposable
    {
        private static readonly Dictionary<IntPtr, Object> objects = new Dictionary<IntPtr, Object>();
        private static readonly Dictionary<Closure, ulong> closures = new Dictionary<Closure, ulong>();
        
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
            // This will automatically register our
            // type in the type dictionary. If the type is
            // a user-subclass, it will register it with
            // the GType type system automatically.
            var t = GetType();
            //var typeId = TypeDictionary.Get(t);
            //Console.WriteLine($"Instantiating {TypeDictionary.Get(typeId)}");
            var typeId = Type.Object; //TODO delete if above code is working
            
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
                var values = new Value[nProps];

                // Populate arrays
                for (int i = 0; i < properties.Length; i++)
                {
                    var prop = properties[i];
                    // TODO: Marshal in a block, rather than one at a time
                    // for performance reasons.
                    names[i] = Marshal.StringToHGlobalAnsi(prop.Name);
                    values[i] = prop.Value;
                }

                // Create with propeties
                handle = Object.new_with_properties(
                    typeId.Value,
                    (uint) names.Length,
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
                handle = Object.new_with_properties(
                    typeId.Value,
                    0,
                    ref zero,
                    Array.Empty<Value>()
                );
            }

            Initialize(handle);
        }

        /// <summary>
        /// Initialises a wrapper for an existing object
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
            objects.Add(ptr, this);
            RegisterProperties();
            RegisterOnFinalized();

            // Allow subclasses to perform initialisation
            Initialize();
        }
        
        /// <summary>
        ///  Wrappers can override here to perform immediate initialisation
        /// </summary>
        protected virtual void Initialize() { }
        
        // Modify this in the future to play nicely with virtual function support?
        private void OnFinalized(IntPtr data, IntPtr where_the_object_was) => Dispose();
        private void RegisterOnFinalized() => Object.weak_ref(Handle, this.OnFinalized, IntPtr.Zero);
        
        private void RegisterProperties()
        {
            const System.Reflection.BindingFlags propertyDescriptorFieldFlags = System.Reflection.BindingFlags.Public 
                | System.Reflection.BindingFlags.Static 
                | System.Reflection.BindingFlags.FlattenHierarchy;
            
            const System.Reflection.BindingFlags methodFlags = System.Reflection.BindingFlags.Instance 
                | System.Reflection.BindingFlags.NonPublic;
            
            foreach (var field in GetType().GetFields(propertyDescriptorFieldFlags))
            {
                if (field.FieldType.IsGenericType && field.FieldType.GetGenericTypeDefinition() == typeof(Property<>))
                {
                    var method = field.FieldType.GetMethod(nameof(Property<Object>.RegisterNotifyEvent), methodFlags);
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
        
        // This function returns the proxy object to the provided handle
        // if it already exists, otherwise creats a new wrapper object
        // and returns it.
        protected static bool TryWrapPointerAs<T>(IntPtr handle, out T o)
        {
            o = default!;

            // Return false if T is not of type Object
            if (!typeof(T).IsSubclassOf(typeof(Object)) && typeof(T) != typeof(Object))
                return false;

            // Attempt to lookup the pointer in the object dictionary
            if (objects.TryGetValue(handle, out var obj))
            {
                o = (T) (object) obj;
                return true;
            }

            // If it is not found, we can assume that it
            // is NOT a subclass type, as we ensure that
            // subclass types always outlive their pointers
            // TODO: Toggle Refs ^^^

            // Resolve gtype of object
            var trueGType = TypeFromHandle(handle);
            var trueType = TypeDictionary.Get(trueGType);

            // Ensure we are not constructing a subclass
            if (IsSubclass(trueType))
                throw new Exception("Encountered foreign subclass pointer! This is a fatal error");

            // Ensure the conversion is valid
            var castGType = TypeDictionary.Get(typeof(T));
            if (!Global.type_is_a(trueGType.Value, castGType.Value))
                throw new InvalidCastException();

            // Create using 'IntPtr' constructor
            var ctor = trueType.GetConstructor(
                System.Reflection.BindingFlags.NonPublic 
                | System.Reflection.BindingFlags.Public 
                | System.Reflection.BindingFlags.Instance,
                null, new[] { typeof(IntPtr) }, null
            );

            o = (T) ctor.Invoke(new object[] { handle });

            // TODO: We don't need the following line as
            // we already add to the object dictionary in the
            // constructor.
            // objects.Add(handle, (Object)(object)o);
            return true;
        }
        
        #endregion

        #region IDisposable

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

                if(Handle != IntPtr.Zero)
                {
                    Object.unref(Handle);
                    objects.Remove(Handle);
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