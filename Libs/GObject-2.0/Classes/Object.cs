using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GLib;
using GObject.Native;

namespace GObject
{
    public partial class Object : IObject, INotifyPropertyChanged, IDisposable, IHandle
    {
        #region Events

        /// <summary>
        /// Event triggered when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion
        
        #region Fields
        private ObjectHandle _handle;
        #endregion

        #region Properties

        public IntPtr Handle => _handle.Handle;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a wrapper for an existing object
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="ownedRef">Defines if the handle is owned by us. If not owned by us it is refed to keep it around.</param>
        protected Object(IntPtr handle, bool ownedRef)
        {
            _handle = new ObjectHandle(handle, this, ownedRef);
            Initialize();
        }

        protected Object(ConstructParameter[] properties)
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// Wrapper and subclasses can override here to perform immediate initialization
        /// </summary>
        protected virtual void Initialize() { }

        public static T WrapHandle<T>(IntPtr handle, bool ownedRef) where T : class
        {
            //TODO REMOVE THIS METHOD
            return default;
        }
        
        protected internal SignalHelper GetSignalHelper(string name)
        {
            return default!;
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

        public virtual void Dispose()
        {
            _handle.Dispose();
        }
    }
}
