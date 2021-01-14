using System;

namespace GLib
{
    public sealed partial class Bytes : IHandle, IDisposable
    {
        #region Properties

        public IntPtr Handle { get; }

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

        #endregion

        private void ReleaseUnmanagedResources()
        {
            var size = get_size(Handle);
            unref(Handle);
            GC.RemoveMemoryPressure((long) size);
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }
    }
}
