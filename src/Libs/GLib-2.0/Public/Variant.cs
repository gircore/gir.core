using System;
using GLib.Internal;

namespace GLib;

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
        Internal.Variant.RefSink(_handle);
    }

    #endregion

    #region Methods

    public static Variant Create(int i) => new(Internal.Variant.NewInt32(i));
    public static Variant Create(uint ui) => new(Internal.Variant.NewUint32(ui));
    public static Variant Create(string str) => new(Internal.Variant.NewString(str));
    public static Variant Create(params string[] strs) => new(Internal.Variant.NewStrv(strs, strs.Length));

    public static Variant CreateEmptyDictionary(VariantType key, VariantType value)
    {
        var childType = Internal.VariantType.NewDictEntry(key.Handle, value.Handle);
        return new Variant(Internal.Variant.NewArray(childType, new IntPtr[0], 0));
    }

    private void Init(out VariantHandle handle, params Variant[] children)
    {
        _children = children;

        var count = (nuint) children.Length;
        var ptrs = new IntPtr[count];

        for (nuint i = 0; i < count; i++)
            ptrs[i] = children[i].Handle.DangerousGetHandle();

        handle = Internal.Variant.NewTuple(ptrs, count);
        Internal.Variant.RefSink(handle);
    }

    public string GetString()
        => StringHelper.ToStringUtf8(Internal.Variant.GetString(_handle, out _));

    public int GetInt()
        => Internal.Variant.GetInt32(_handle);

    public uint GetUInt()
        => Internal.Variant.GetUint32(_handle);

    public string Print(bool typeAnnotate)
        => Internal.Variant.Print(_handle, typeAnnotate);

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
    public static VariantHandle GetSafeHandle(this Variant? variant)
        => variant is null ? VariantNullHandle.Instance : variant.Handle;
}
