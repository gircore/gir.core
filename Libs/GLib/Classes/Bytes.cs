using System;

namespace GLib
{
    public sealed partial class Bytes : IHandle, IDisposable
    {
        #region Fields

        private readonly long _size;
        private readonly BytesSafeHandle _safeHandle;
        
        #endregion
        
        #region Properties

        public IntPtr Handle => _safeHandle?.DangerousGetHandle() ?? IntPtr.Zero;

        #endregion

        #region Constructors

        private Bytes(IntPtr handle)
        {
            _safeHandle = new BytesSafeHandle(handle);
            _size = (long) get_size(handle);
            GC.AddMemoryPressure(_size);
        }

        #endregion

        #region Methods

        public static Bytes From(byte[] data)
        {
            var obj = new Bytes(@new(data, (ulong) data.Length));
            return obj;
        }

        public void Dispose()
        {
            _safeHandle.Dispose();
            GC.RemoveMemoryPressure(_size);
        }

        #endregion
    }
}
