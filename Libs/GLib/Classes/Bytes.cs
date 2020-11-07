using System;

namespace GLib
{
    public partial class Bytes
    {
        private IntPtr _handle;

        internal Bytes(IntPtr handle)
        {
            _handle = handle;
        }

        public static Bytes From(byte[] data)
        {
             return new Bytes(Native.@new(data, (ulong) data.Length));
        }

        //TODO: Get rid of this
        public static explicit operator IntPtr(Bytes b) => b._handle;
    }
}
