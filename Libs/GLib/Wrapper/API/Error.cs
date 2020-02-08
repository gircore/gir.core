using System;
using System.Runtime.InteropServices;

namespace GLib
{
    [StructLayout(LayoutKind.Sequential)]
    public partial struct Error
    {
        public int Domain;
        public int Code;
        public IntPtr Message;
    }
}