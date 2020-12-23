using System;
using GLib.Interfaces;

namespace GLib
{
    public partial class Bytes : IHandle
    {
        #region Properties

        public IntPtr Handle { get; }

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
            return new Bytes(@new(data, (ulong) data.Length));
        }

        #endregion

    }
}
