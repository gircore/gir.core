using System;

namespace GLib
{
    public sealed partial class Bytes : IDisposable
    {
        #region Fields

        private long _size;

        #endregion

        #region Constructors

        partial void Initialize()
        {
            _size = (long) Native.Bytes.Methods.GetSize(_handle);
            GC.AddMemoryPressure(_size);
        }

        #endregion

        #region Methods

        public static Bytes From(byte[] data)
        {
            var obj = new Bytes(Native.Bytes.Methods.New(data, (nuint) data.Length));
            return obj;
        }

        public void Dispose()
        {
            _handle.Dispose();
            GC.RemoveMemoryPressure(_size);
        }

        #endregion
    }
}
