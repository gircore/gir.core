using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GLib.Core
{
    public partial class GVariant
    {
        private GVariant[] children;

        #region Properties
        private readonly IntPtr handle;
        public IntPtr Handle => handle;
        #endregion Properties

        public GVariant(int i) : this(GLib.Variant.new_int32(i)) { }
        public GVariant(uint ui) : this(GLib.Variant.new_uint32(ui)){ }
        public GVariant(string str) : this(GLib.Variant.new_string(str)) { }
        public GVariant(params string[] strs) : this(GLib.Variant.new_strv(strs, strs.Length)) { }

        public GVariant(params GVariant[] children)
        {
            this.children = children;
            Init(out this.handle, children);
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
            this.children = data;
            Init(out this.handle, data);
        }

        public GVariant(IntPtr handle)
        {
            children = new GVariant[0];
            this.handle = handle;
            GLib.Variant.ref_sink(handle);
        }

        public static GVariant CreateEmptyDictionary(GVariantType key, GVariantType value)
        {
            var childType = GLib.VariantType.new_dict_entry(key.Handle, value.Handle);
            return new GVariant(GLib.Variant.new_array(childType, new IntPtr[0], 0));
        }

        private void Init(out IntPtr handle, params GVariant[] children)
        {
            this.children = children;

            var count = children.Length;
            var ptrs = new IntPtr[count];

            for(int i = 0; i < count; i++)
                ptrs[i] = children[i].Handle;
            
            handle = GLib.Variant.new_tuple(ptrs, (ulong) count);
            GLib.Variant.ref_sink(handle);
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