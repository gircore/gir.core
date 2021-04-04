using System;

namespace GLib
{
    public sealed partial record Bytes : IDisposable
    {
        #region Fields

        private readonly long _size;
        private readonly Native.Bytes.Handle _safeHandle;

        #endregion

        #region Constructors

        private Bytes(Native.Bytes.Handle handle)
        {
            _safeHandle = handle;
            _size = (long) Native.Bytes.Methods.GetSize(handle);
            GC.AddMemoryPressure(_size);
        }

        #endregion

        #region Methods

        public static Bytes From(byte[] data)
        {
            var obj = new Bytes(Native.Bytes.Methods.New(data, (ulong) data.Length));
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
