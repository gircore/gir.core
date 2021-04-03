using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GLib;
using GObject.Native;

#nullable enable

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

        #region Properties

        public IntPtr Handle { get; private set; }

        #endregion

        #region Constructors

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
                Native.Instance.Methods.RefSink(handle);
            }
            else
            {
                //In case we own the ref because the ownership was fully transfered to us we
                //do not need to ref the object at all.

                Debug.Assert(!Native.Instance.Methods.IsFloating(handle), "Owned floating references are not possible.");
            }

            Initialize(handle);
        }

        private void Initialize(IntPtr handle)
        {
            Handle = handle;
            ObjectMapper.Map(handle, this);
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
            //TODO
        }
    }
}
