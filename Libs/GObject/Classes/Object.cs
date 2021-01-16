using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using GLib;

namespace GObject
{
    public partial class Object : IObject, INotifyPropertyChanged, IDisposable, IHandle
    {
        #region Fields

        private readonly Dictionary<string, SignalHelper> _signals = new ();
        
        #endregion

        #region Events

        /// <summary>
        /// Event triggered when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion

        #region Properties
        
        public IntPtr Handle { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new object
        /// </summary>
        /// <param name="properties"></param>
        /// <remarks>This constructor is protected to be sure that there is no caller (enduser) keeping a reference to
        /// the construct parameters as the contained values are freed at the end of this constructor.
        /// If certain constructors are needed they need to be implemented with concrete constructor arguments in
        /// a higher layer.</remarks>
        protected Object(params ConstructParameter[] properties)
        {
            // This will automatically register our
            // type in the type dictionary. If the type is
            // a user-subclass, it will register it with
            // the GType type system automatically.
            System.Type? t = GetType();
            Type typeId = TypeDictionary.Get(t);
            Console.WriteLine($"Instantiating {TypeDictionary.Get(typeId)}");

            var names = new IntPtr[properties.Length];
            var values = new Value[properties.Length];

            IntPtr handle;

            try
            {
                for (var i = 0; i < properties.Length; i++)
                {
                    ConstructParameter? prop = properties[i];
                    // TODO: Marshal in a block, rather than one at a time
                    // for performance reasons.
                    names[i] = Marshal.StringToHGlobalAnsi(prop.Name);
                    values[i] = prop.Value;
                }

                IntPtr zero = IntPtr.Zero;

                handle = Native.new_with_properties(
                    typeId.Value,
                    (uint) properties.Length,
                    ref (properties.Length > 0 ? ref names[0] : ref zero),
                    values
                );

                if (Native.is_floating(handle))
                    Native.ref_sink(handle); //Make sure handle is owned by us
            }
            finally
            {
                foreach (IntPtr ptr in names)
                    Marshal.FreeHGlobal(ptr);

                foreach (Value value in values)
                    value.Dispose();   
            }

            Initialize(handle);
        }

        /// <summary>
        /// Initializes a wrapper for an existing object
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="ownedRef">Defines if the handle is owned by us. If not owned by us it is refed to keep it around.</param>
        protected Object(IntPtr handle, bool ownedRef)
        {
            if (!ownedRef)
            {
                // - Unowned GObjects need to be refed to bind them to this instance
                // - Unowned InitiallyUnowned floating objects need to be ref_sinked
                // - Unowned InitiallyUnowned non-floating objects need to be refed
                // As ref_sink behaves like ref in case of non floating instances we use it for all 3 cases
                Native.ref_sink(handle);
            }
            else
            {
                //In case we own the ref because the ownership was fully transfered to us we
                //do not need to ref the object at all.

                Debug.Assert(!Native.is_floating(handle), "Owned floating references are not possible.");
            }

            Initialize(handle);
        }

        ~Object()
        {
            Dispose(false);
        }

        #endregion

        #region Methods

        private void Initialize(IntPtr ptr)
        {
            Handle = ptr;

            ReferenceManager.RegisterObject(this);
            RegisterProperties();

            Initialize();
        }

        /// <summary>
        /// Wrapper and subclasses can override here to perform immediate initialization
        /// </summary>
        protected virtual void Initialize() { }

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

        protected internal SignalHelper GetSignalHelper(string name)
        {
            if(_signals.TryGetValue(name, out var signalHelper))
                return signalHelper;

            signalHelper = new SignalHelper(this, name);
            _signals.Add(name, signalHelper);
            
            return signalHelper;
        }

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

        #endregion

        #region IDisposable Implementation

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (Handle != IntPtr.Zero)
            {
                foreach (var signalHelper in _signals.Values)
                    signalHelper.Dispose();
                
                _signals.Clear();

                ReferenceManager.RemoveObject(this);

                Native.unref(Handle);
                Handle = IntPtr.Zero;
            }
        }

        #endregion
    }
}
