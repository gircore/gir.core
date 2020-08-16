using System;
using System.Runtime.InteropServices;

namespace GObject.Sys
{
    [StructLayout(LayoutKind.Sequential)]
    public partial struct TypeQuery {
        public IntPtr type;
        public IntPtr type_name;
        public uint class_size;
        public uint instance_size;
    }
}