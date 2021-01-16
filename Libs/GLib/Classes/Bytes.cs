using System;

namespace GLib
{
    public sealed partial class Bytes : IHandle, IDisposable
    {
        #region Properties

        public IntPtr Handle { get; private set; }

        #endregion

        #region Constructors

        private Bytes(IntPtr handle)
        {
            Handle = handle;
            GC.AddMemoryPressure((long) get_size(handle));
        }

        ~Bytes()
        {
            ReleaseUnmanagedResources();
        }

        #endregion

        #region Methods

        public static Bytes From(byte[] data)
        {
            var obj = new Bytes(@new(data, (ulong) data.Length));
            return obj;
        }

        private void ReleaseUnmanagedResources()
        {
            if (Handle != IntPtr.Zero)
            {
                var size = get_size(Handle);
                unref(Handle);
                Handle = IntPtr.Zero;
                GC.RemoveMemoryPressure((long) size);
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
