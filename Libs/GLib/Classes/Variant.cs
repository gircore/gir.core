using System;
using System.Runtime.InteropServices;

namespace GLib
{
    public partial class Variant
    {
        #region Fields

        private Variant[] _children;
        private readonly IntPtr _handle;

        #endregion

        #region Properties

        public IntPtr Handle => _handle;

        #endregion

        #region Constructors

        public Variant(params Variant[] children)
        {
            _children = children;
            Init(out _handle, children);
        }

        /*public Variant(IDictionary<string, Variant> dictionary)
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
        }*/

        public Variant(IntPtr handle)
        {
            _children = new Variant[0];
            _handle = handle;
            Native.ref_sink(handle);
        }

        #endregion

        #region Methods

        public static Variant Create(int i) => new Variant(Native.new_int32(i));
        public static Variant Create(uint ui) => new Variant(Native.new_uint32(ui));
        public static Variant Create(string str) => new Variant(Native.new_string(str));
        public static Variant Create(params string[] strs) => new Variant(Native.new_strv(strs, strs.Length));

        public static Variant CreateEmptyDictionary(VariantType key, VariantType value)
        {
            IntPtr childType = VariantType.Native.new_dict_entry(key.Handle, value.Handle);
            return new Variant(Native.new_array(childType, new IntPtr[0], 0));
        }

        private void Init(out IntPtr handle, params Variant[] children)
        {
            _children = children;

            var count = children.Length;
            var ptrs = new IntPtr[count];

            for (var i = 0; i < count; i++)
                ptrs[i] = children[i].Handle;

            handle = Native.new_tuple(ptrs, (ulong) count);
            Native.ref_sink(handle);
        }

        public string GetString()
        {
            ulong length = 0;
            IntPtr strPtr = Native.get_string(_handle, ref length);

            return Marshal.PtrToStringAuto(strPtr);
        }

        public string Print(bool typeAnnotate)
            => Marshal.PtrToStringAuto(Native.print(_handle, typeAnnotate));

        #endregion
    }
}
