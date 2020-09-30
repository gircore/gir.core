using System;
using System.Runtime.InteropServices;

namespace GObject
{
    [StructLayout(LayoutKind.Sequential)]
	public partial struct Closure
    {
		public long fields;
		public IntPtr marshal;
		public IntPtr data;
		public IntPtr notifiers;
    }
}