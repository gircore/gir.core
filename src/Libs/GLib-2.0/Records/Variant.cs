using System;
using GLib.Internal;

namespace GLib
{
    public partial class Variant : IDisposable
    {
        #region Fields

        private Variant[] _children;

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

        partial void Initialize()
        {
            _children = new Variant[0];
            Internal.Variant.Methods.RefSink(_handle);
        }

        #endregion

        #region Methods

        public static Variant Create(int i) => new Variant(Internal.Variant.Methods.NewInt32(i));
        public static Variant Create(uint ui) => new Variant(Internal.Variant.Methods.NewUint32(ui));
        public static Variant Create(string str) => new Variant(Internal.Variant.Methods.NewString(str));
        public static Variant Create(params string[] strs) => new Variant(Internal.Variant.Methods.NewStrv(strs, strs.Length));

        public static Variant CreateEmptyDictionary(VariantType key, VariantType value)
        {
            var childType = Internal.VariantType.Methods.NewDictEntry(key.Handle, value.Handle);
            return new Variant(Internal.Variant.Methods.NewArray(childType, new IntPtr[0], 0));
        }

        private void Init(out Internal.Variant.Handle handle, params Variant[] children)
        {
            _children = children;

            var count = (nuint) children.Length;
            var ptrs = new IntPtr[count];

            for (nuint i = 0; i < count; i++)
                ptrs[i] = children[i].Handle.DangerousGetHandle();

            handle = Internal.Variant.Methods.NewTuple(ptrs, count);
            Internal.Variant.Methods.RefSink(handle);
        }

        public string GetString()
            => StringHelper.ToStringUtf8(Internal.Variant.Methods.GetString(_handle, out _));

        public int GetInt()
            => Internal.Variant.Methods.GetInt32(_handle);

        public uint GetUInt()
            => Internal.Variant.Methods.GetUint32(_handle);

        public string Print(bool typeAnnotate)
            => Internal.Variant.Methods.Print(_handle, typeAnnotate);

        #endregion

        public void Dispose()
        {
            foreach (var child in _children)
                child.Dispose();

            Handle.Dispose();
        }
    }

    public static class VariantExtension
    {
        public static Internal.Variant.Handle GetSafeHandle(this Variant? variant)
            => variant is null ? Internal.Variant.Handle.Null : variant.Handle;
    }
}
