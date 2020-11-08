using System;

namespace GLib
{
    public partial class Bytes
    {
        #region Properties

        internal IntPtr Handle { get; }

        #endregion

        #region Constructors

        private Bytes(IntPtr handle)
        {
            Handle = handle;
        }

        #endregion

        #region Methods

        public static Bytes From(byte[] data)
        {
            return new Bytes(Native.@new(data, (ulong) data.Length));
        }

        #endregion

    }
}
