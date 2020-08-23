using System;
using System.Runtime.InteropServices;

namespace GObject
{
    public partial class Object
    {
        // ABI for retrieving type
        [StructLayout(LayoutKind.Sequential)]
        private struct GTypeClass
        {
            public ulong gtype;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct GTypeInstance
        {
            public IntPtr g_class;
        }
    }
}