using System;
using System.Runtime.InteropServices;

namespace GLib.Core
{
    public partial class GVariant
    {
        private readonly IntPtr handle;
        public IntPtr Handle => handle;

        public GVariant(int i) : this(GLib.Variant.new_int32(i)) { }
        public GVariant(uint ui) : this(GLib.Variant.new_uint32(ui)){ }
        public GVariant(string str) : this(GLib.Variant.new_string(str)) { }

        public GVariant(GVariant[] children)
        {
            var count = children.LongLength;
            var ptrs = new IntPtr[count];
            for(int i = 0; i < count; i++)
                ptrs[i] = children[i].Handle;
            
            this.handle = GLib.Variant.new_tuple(ptrs, (ulong) count);
        }

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