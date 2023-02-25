using System;
using System.Runtime.InteropServices;
using GLib.Internal;
using GObject.Internal;

namespace GObject;

// TODO: Consider splitting value into different types for each type it represents
// to avoid breaking the open closed principle.
// There could an abstract value base class with generic implementations of the concrete types.
public partial class Value : IDisposable
{
    #region Constructors

    public Value(Type type)
    {
        _handle = ValueManagedHandle.Create();

        // We ignore the return parameter as it is a pointer
        // to the same location like the instance parameter.
        _ = Init(_handle, type.Value);
    }

    public Value(Object value) : this(Type.Object) => SetObject(value);
    public Value(GLib.Variant value) : this(Type.Variant) => SetVariant(value);
    public Value(bool value) : this(Type.Boolean) => SetBoolean(value);
    public Value(int value) : this(Type.Int) => SetInt(value);
    public Value(uint value) : this(Type.UInt) => SetUint(value);
    public Value(long value) : this(Type.Long) => SetLong(value);
    public Value(double value) : this(Type.Double) => SetDouble(value);
    public Value(float value) : this(Type.Float) => SetFloat(value);
    public Value(string value) : this(Type.String) => SetString(value);

    #endregion

    #region Methods

    internal ValueData GetData() => Marshal.PtrToStructure<ValueData>(Handle.DangerousGetHandle());

    private nuint GetTypeValue()
    {
        var structure = Marshal.PtrToStructure<ValueData>(Handle.DangerousGetHandle());
        return structure.GType;
    }

    /// <summary>
    /// Gets an instance of <see cref="Value"/> from the given <paramref name="value"/>.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="Value"/> if the cast is successful.
    /// </returns>
    /// <exception cref="NotSupportedException">
    /// The given <paramref name="value"/> has a type which cannot be parsed as a <see cref="Value"/>.
    /// </exception>
    public static Value From(object value) => value switch
    {
        bool v1 => new Value(v1),
        uint v2 => new Value(v2),
        int v3 => new Value(v3),
        long v4 => new Value(v4),
        double v5 => new Value(v5),
        float v6 => new Value(v6),
        string v7 => new Value(v7),
        Enum _ => new Value((long) value),
        GLib.Variant v => new Value(v),
        Object obj => new Value(obj),
        _ => throw new NotSupportedException("Unable to create the value from the given type.")
    };

    public void Set(object? value)
    {
        switch (value)
        {
            case bool b:
                SetBoolean(b);
                break;
            case uint u:
                SetUint(u);
                break;
            case int i:
                SetInt(i);
                break;
            case string s:
                SetString(s);
                break;
            case double d:
                SetDouble(d);
                break;
            case Enum e:
                SetEnum(e);
                break;
            case long l:
                SetLong(l);
                break;
            case float f:
                SetFloat(f);
                break;
            case string[] array:
                // Marshalling logic happens inside this safe handle. GValue takes a
                // copy of the boxed memory so we do not need to keep it alive. The
                // Garbage Collector will automatically free the safe handle for us.
                var strArray = new StringArrayNullTerminatedSafeHandle(array);
                SetBoxed(strArray.DangerousGetHandle());
                break;
            case GLib.Variant v:
                SetVariant(v);
                break;
            case Object o:
                SetObject(o);
                break;
            case null:
                break;
            default:
                throw new NotSupportedException($"Type {value.GetType()} is not supported as a value type");
        }
    }

    /// <summary>
    /// Extracts the content of this <see cref="Value"/> into an object.
    /// </summary>
    /// <returns>The content of this wrapped in an object</returns>
    /// <exception cref="NotSupportedException">
    /// The value cannot be casted to the given type.
    /// </exception>
    public object? Extract()
    {
        var type = GetTypeValue();
        return type switch
        {
            (nuint) BasicType.Boolean => GetBool(),
            (nuint) BasicType.UInt => GetUint(),
            (nuint) BasicType.Int => GetInt(),
            (nuint) BasicType.Long => GetLong(),
            (nuint) BasicType.Double => GetDouble(),
            (nuint) BasicType.Float => GetFloat(),
            (nuint) BasicType.String => GetString(),
            (nuint) BasicType.Pointer => GetPtr(),
            _ => CheckComplexTypes(type)
        };
    }

    private object? CheckComplexTypes(nuint gtype)
    {
        if (Functions.TypeIsA(gtype, (nuint) BasicType.Object))
            return GetObject();

        if (Functions.TypeIsA(gtype, (nuint) BasicType.Boxed))
            return GetBoxed(gtype);

        if (Functions.TypeIsA(gtype, (nuint) BasicType.Enum))
            return GetEnum();

        if (Functions.TypeIsA(gtype, (nuint) BasicType.Flags))
            return GetFlags();

        if (Functions.TypeIsA(gtype, (nuint) BasicType.Param))
            return GetParam();

        if (Functions.TypeIsA(gtype, (nuint) BasicType.Variant))
            return GetVariant();

        var name = Internal.Functions.TypeName(gtype).ConvertToString();

        throw new NotSupportedException($"Unable to extract the value for type '{name}'. The type (id: {gtype}) is unknown.");
    }

    public T Extract<T>() => (T) Extract()!;

    public IntPtr GetPtr() => Internal.Value.GetPointer(Handle);

    public ParamSpec GetParam()
    {
        var paramHandle = Internal.Value.GetParam(Handle);
        return new ParamSpec(paramHandle);
    }

    public object? GetBoxed(nuint type)
    {
        IntPtr ptr = Internal.Value.GetBoxed(Handle);

        if (type == Internal.Functions.StrvGetType())
            return StringHelper.ToStringArrayUtf8(ptr);

        // TODO: It would be nice to support boxing arbitrary managed types
        // One idea for how to achieve this is creating our own 'OpaqueBoxed' type
        // which wraps a GCHandle or similar. We can then retrieve this at runtime
        // from a static dictionary, etc. Alternatively, perhaps we want to find a
        // method which plays nice with AOT compilation.

        // TODO: Should this be GetBoxed/TakeBoxed/DupBoxed? 
        return BoxedWrapper.WrapHandle(
            handle: Internal.Value.GetBoxed(Handle),
            ownsHandle: false,
            gtype: new Type(type)
        );
    }

    public Object? GetObject()
        => ObjectWrapper.WrapNullableHandle<Object>(Internal.Value.GetObject(Handle), false);

    public bool GetBool() => Internal.Value.GetBoolean(Handle);
    public uint GetUint() => Internal.Value.GetUint(Handle);
    public int GetInt() => Internal.Value.GetInt(Handle);
    public long GetLong() => Internal.Value.GetLong(Handle);
    public double GetDouble() => Internal.Value.GetDouble(Handle);
    public float GetFloat() => Internal.Value.GetFloat(Handle);
    public ulong GetFlags() => Internal.Value.GetFlags(Handle);
    public long GetEnum() => Internal.Value.GetEnum(Handle);
    public string? GetString() => GetString(Handle).ConvertToString();
    public GLib.Variant? GetVariant()
    {
        var result = Internal.Value.GetVariant(Handle);
        return result.IsInvalid ? null : new(result);
    }

    private void SetBoxed(IntPtr ptr) => Internal.Value.SetBoxed(Handle, ptr);
    private void SetBoolean(bool b) => Internal.Value.SetBoolean(Handle, b);
    private void SetUint(uint u) => Internal.Value.SetUint(Handle, u);
    private void SetInt(int i) => Internal.Value.SetInt(Handle, i);
    private void SetDouble(double d) => Internal.Value.SetDouble(Handle, d);
    private void SetFloat(float f) => Internal.Value.SetFloat(Handle, f);
    private void SetLong(long l) => Internal.Value.SetLong(Handle, l);
    private void SetEnum(Enum e) => Internal.Value.SetEnum(Handle, Convert.ToInt32(e));
    private void SetString(string s) => Internal.Value.SetString(Handle, GLib.Internal.NullableUtf8StringOwnedHandle.Create(s));
    private void SetVariant(GLib.Variant v) => Internal.Value.SetVariant(Handle, v.Handle);
    private void SetObject(Object o) => Internal.Value.SetObject(Handle, o.Handle);

    public void Dispose()
    {
        Handle.Dispose();
    }

    #endregion

    #region Internal

    // This redeclares the "g_value_init" method. The internal method
    // returns a GObject.Internal.Value.Handle which can not be freed
    // via a free function. The Marshaller would create an instance
    // of GObject.Internal.Value.Handle and the GC would try to
    // dispose it which is not possible and throws an Exception. To
    // avoid the GObject.Internal.Value.Handle creation this method
    // returns just an IntPtr. It is okay to return an IntPtr as the
    // returned IntPtr points to the location of the "value" parameter.
    [DllImport(ImportResolver.Library, EntryPoint = "g_value_init")]
    private static extern IntPtr Init(GObject.Internal.ValueHandle value, nuint gType);

    //TODO: g_value_get_string get's redeclared here as it is not annotated correctly.
    //Remove after release of: https://gitlab.gnome.org/GNOME/glib/-/merge_requests/3301
    //Use "Internal.Value.GetString(Handle).ConvertToString();" again
    [DllImport(ImportResolver.Library, EntryPoint = "g_value_get_string")]
    public static extern GLib.Internal.NullableUtf8StringUnownedHandle GetString(GObject.Internal.ValueHandle value);

    #endregion
}
