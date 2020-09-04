using System;
using System.Runtime.InteropServices;

namespace GLib
{
    [StructLayout(LayoutKind.Sequential)]
    public partial struct Error
    {
        #region Fields

        public int Domain;
        public int Code;
        public IntPtr Message;

        #endregion
        
        #region Methods

        public void Free() => free(ref this);

        #endregion
    }
}