using System;
using System.Runtime.CompilerServices;
using GLib.Internal;
using GObject.Internal;

namespace GObject;

public partial class Value : IDisposable
{
    public Value(Type type)
    {
        Handle = ValueManagedHandle.Create();

        // We ignore the return parameter as it is a pointer
        // to the same location like the instance parameter.
        _ = Internal.Value.Init(Handle, type);
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
    public Value(string[] value) : this(Type.StringArray) => SetBoxed(Utf8StringArrayNullTerminatedOwnedHandle.Create(value).DangerousGetHandle());

    public Value(Enum value) : this(HasFlags(value) ? Type.Flags : Type.Enum)
    {
        if (HasFlags(value))
            SetFlags(value);
        else
            SetEnum(value);
    }

    private static bool HasFlags(Enum e) => e.GetType().IsDefined(typeof(FlagsAttribute), false);

    private unsafe nuint GetTypeValue()
    {
        //The first element of the structure the pointer points to is the gtype of the value
        return Unsafe.AsRef<nuint>((void*) Handle.DangerousGetHandle());
    }

    /// <summary>
    /// Extracts the content of this <see cref="Value"/> into an object.
    /// </summary>
    /// <returns>The content of this wrapped in an object</returns>
    /// <exception cref="NotSupportedException">
    /// The value cannot be casted to the given type.
    /// </exception>
    internal object? Extract()
    {
        var type = GetTypeValue();
        return type switch
        {
            (nuint) BasicType.Boolean => GetBoolean(),
            (nuint) BasicType.UInt => GetUint(),
            (nuint) BasicType.Int => GetInt(),
            (nuint) BasicType.Long => GetLong(),
            (nuint) BasicType.Double => GetDouble(),
            (nuint) BasicType.Float => GetFloat(),
            (nuint) BasicType.String => GetString(),
            (nuint) BasicType.Pointer => GetPointer(),
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

    internal T Extract<T>() => (T) Extract()!;

    public object? GetBoxed(nuint type)
    {
        var ptr = Internal.Value.GetBoxed(Handle);

        if (ptr == IntPtr.Zero)
            return null;

        if (type == Internal.Functions.StrvGetType())
            return new Utf8StringArrayNullTerminatedUnownedHandle(ptr).ConvertToStringArray();

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

    public string[]? GetStringArray()
    {
        var type = GetTypeValue();
        if (type != Internal.Functions.StrvGetType())
        {
            var typeName = Internal.Functions.TypeName(type).ConvertToString();
            throw new Exception($"Value does not hold a string array, but a '{typeName}'");
        }

        var ptr = Internal.Value.GetBoxed(Handle);

        return ptr == IntPtr.Zero
            ? null
            : new Utf8StringArrayNullTerminatedUnownedHandle(ptr).ConvertToStringArray();
    }

    public T GetFlags<T>() where T : Enum
    {
        return (T) Enum.ToObject(typeof(T), Internal.Value.GetFlags(Handle));
    }

    public T GetEnum<T>() where T : Enum
    {
        return (T) Enum.ToObject(typeof(T), Internal.Value.GetEnum(Handle));
    }

    public void SetEnum(Enum e) => Internal.Value.SetEnum(Handle, Convert.ToInt32(e));
    public void SetFlags(Enum e) => Internal.Value.SetFlags(Handle, Convert.ToUInt32(e));

    internal void Set(object? value)
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
                if (HasFlags(e))
                    SetFlags(e);
                else
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
                var strArray = Utf8StringArrayNullTerminatedOwnedHandle.Create(array);
                SetBoxed(strArray.DangerousGetHandle());
                break;
            case GLib.BoxedRecord b:
                SetBoxed(b.GetHandle());
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
}
