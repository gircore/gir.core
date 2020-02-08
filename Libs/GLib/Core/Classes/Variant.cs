using System;
using System.Runtime.InteropServices;

namespace GLib.Core
{
    public class GVariant
    {
        private readonly IntPtr handle;

        internal GVariant(IntPtr handle)
        {
            this.handle = handle;
        }

        public string GetString()
        {
            ulong length = 0;
            var strPtr = GLib.Variant.get_string(handle, ref length);

            var text = Marshal.PtrToStringAuto(strPtr);
            return text;
        }
    }
}