using System;

namespace GLib
{
    public sealed partial record Bytes : IDisposable
    {
        #region Fields

        private readonly long _size;
        private readonly Native.BytesSafeHandle _safeHandle;

        #endregion

        #region Constructors

        private Bytes(Native.BytesSafeHandle handle)
        {
            _safeHandle = handle;
            _size = (long) Native.Methods.GetSize(handle);
            GC.AddMemoryPressure(_size);
        }

        #endregion

        #region Methods

        public static Bytes From(byte[] data)
        {
            var obj = new Bytes(Native.Methods.New(data, (ulong) data.Length));
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
