using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GLib
{
    public partial class Variant
    {/*
        private Variant[] children;

        #region Properties
        private readonly IntPtr handle;
        public IntPtr Handle => handle;
        #endregion Properties

        public static Variant Create(int i) => new Variant(Variant.new_int32(i));
        public static Variant Create(uint ui) => new Variant(Variant.new_uint32(ui));
        public static Variant Create(string str) => new Variant(Variant.new_string(str));
        public static Variant Create(params string[] strs) => new Variant(Variant.new_strv(strs, strs.Length));

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
                var e = new Variant(Variant.new_dict_entry(new Variant(entry.Key).Handle, entry.Value.handle));
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
            Variant.ref_sink(handle);
        }

        public static Variant CreateEmptyDictionary(VariantType key, VariantType value)
        {
            var childType = VariantType.new_dict_entry(key.Handle, value.Handle);
            return new Variant(Variant.new_array(childType, new IntPtr[0], 0));
        }

        private void Init(out IntPtr handle, params Variant[] children)
        {
            this.children = children;

            var count = children.Length;
            var ptrs = new IntPtr[count];

            for(int i = 0; i < count; i++)
                ptrs[i] = children[i].Handle;
            
            handle = Variant.new_tuple(ptrs, (ulong) count);
            Variant.ref_sink(handle);
        }

        public string GetString()
        {
            ulong length = 0;
            var strPtr = Variant.get_string(handle, ref length);

            var text = Marshal.PtrToStringAuto(strPtr);
            return text;
        }

        public string Print(bool typeAnnotate)
            => Marshal.PtrToStringAuto(Variant.print(handle, typeAnnotate));*/
    }
}