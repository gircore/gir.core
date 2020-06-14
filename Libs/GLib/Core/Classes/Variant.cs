using System;
using System.Runtime.InteropServices;

namespace GLib.Core
{
    public partial class GVariant
    {
        private readonly IntPtr handle;

        public GVariant(IntPtr handle)
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

        public string Print(bool typeAnnotate)
            => Marshal.PtrToStringAuto(GLib.Variant.print(handle, typeAnnotate));
    }
}