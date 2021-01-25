using System;

namespace GLib
{
    public sealed partial class Bytes : IHandle, IDisposable
    {
        #region Fields

        private readonly long _size;
        
        #endregion
        
        #region Properties

        public IntPtr Handle { get; private set; }

        #endregion

        #region Constructors

        private Bytes(IntPtr handle)
        {
            Handle = handle;
            _size = (long) Native.get_size(handle);
            GC.AddMemoryPressure(_size);
        }

        ~Bytes()
        {
            ReleaseUnmanagedResources();
        }

        #endregion

        #region Methods

        public static Bytes From(byte[] data)
        {
            var obj = new Bytes(Native.@new(data, (ulong) data.Length));
            return obj;
        }

        private void ReleaseUnmanagedResources()
        {
            if (Handle != IntPtr.Zero)
            {
                Native.unref(Handle);
                Handle = IntPtr.Zero;
                GC.RemoveMemoryPressure(_size);
            }
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
