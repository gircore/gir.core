using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GLib.Core
{
    public partial class GVariant
    {
        private readonly IntPtr handle;
        public IntPtr Handle => handle;

        private GVariant[] children = default;

        public GVariant(int i) : this(GLib.Variant.new_int32(i)) { }
        public GVariant(uint ui) : this(GLib.Variant.new_uint32(ui)){ }
        public GVariant(string str) : this(GLib.Variant.new_string(str)) { }
        public GVariant(params string[] strs) : this(GLib.Variant.new_strv(strs, strs.Length)) { }

        public GVariant(params GVariant[] children)
        {
            Init(out this.handle, children);
        }
        public GVariant(IntPtr handle)
        {
            this.handle = handle;
            GLib.Variant.ref_sink(handle);
        }

        public GVariant(IDictionary<string, GVariant> dictionary)
        {
            var data = new GVariant[dictionary.Count];
            var counter = 0;
            foreach(var entry in dictionary)
            {
                var e = new GVariant(GLib.Variant.new_dict_entry(new GVariant(entry.Key).Handle, entry.Value.handle));
                data[counter] = e;
                counter++;
            }
            Init(out this.handle, data);
        }

        public static GVariant CreateEmptyDictionary(GVariantType key, GVariantType value)
        {
            var childType = GLib.VariantType.new_dict_entry(key.Handle, value.Handle);
            return new GVariant(GLib.Variant.new_array(childType, new IntPtr[0], 0));
        }

        private void Init(out IntPtr handle, params GVariant[] children)
        {
            var count = children.LongLength;
            var ptrs = new IntPtr[count];
            for(int i = 0; i < count; i++)
                ptrs[i] = children[i].Handle;
            
            handle = GLib.Variant.new_tuple(ptrs, (ulong) count);

            this.children = children;
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