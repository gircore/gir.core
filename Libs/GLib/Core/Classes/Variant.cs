using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GLib
{
    public partial class Variant
    {
        private Variant[] children;

        #region Properties
        private readonly IntPtr handle;
        public IntPtr Handle => handle;
        #endregion Properties

        public Variant(int i) : this(Sys.Variant.new_int32(i)) { }
        public Variant(uint ui) : this(Sys.Variant.new_uint32(ui)){ }
        public Variant(string str) : this(Sys.Variant.new_string(str)) { }
        public Variant(params string[] strs) : this(Sys.Variant.new_strv(strs, strs.Length)) { }

        public Variant(params Variant[] children)
        {
            this.children = children;
            Init(out this.handle, children);
        }

        public Variant(IDictionary<string, Variant> dictionary)
        {
            var data = new Variant[dictionary.Count];
            var counter = 0;
            foreach(var entry in dictionary)
            {
                var e = new Variant(Sys.Variant.new_dict_entry(new Variant(entry.Key).Handle, entry.Value.handle));
                data[counter] = e;
                counter++;
            }
            this.children = data;
            Init(out this.handle, data);
        }

        public Variant(IntPtr handle)
        {
            children = new Variant[0];
            this.handle = handle;
            Sys.Variant.ref_sink(handle);
        }

        public static Variant CreateEmptyDictionary(VariantType key, VariantType value)
        {
            var childType = Sys.VariantType.new_dict_entry(key.Handle, value.Handle);
            return new Variant(Sys.Variant.new_array(childType, new IntPtr[0], 0));
        }

        private void Init(out IntPtr handle, params Variant[] children)
        {
            this.children = children;

            var count = children.Length;
            var ptrs = new IntPtr[count];

            for(int i = 0; i < count; i++)
                ptrs[i] = children[i].Handle;
            
            handle = Sys.Variant.new_tuple(ptrs, (ulong) count);
            Sys.Variant.ref_sink(handle);
        }

        public string GetString()
        {
            ulong length = 0;
            var strPtr = Sys.Variant.get_string(handle, ref length);

            var text = Marshal.PtrToStringAuto(strPtr);
            return text;
        }

        public string Print(bool typeAnnotate)
            => Marshal.PtrToStringAuto(Sys.Variant.print(handle, typeAnnotate));
    }
}