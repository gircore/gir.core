using System;
using System.Runtime.InteropServices;

namespace GObject;

/// <summary>
/// Describes a GProperty.
/// </summary>
/// <typeparam name="T">The type of the value this property will store.</typeparam>
/// <typeparam name="K">The type of the class / interface implementing this property.</typeparam>
public sealed class Property<T, K> : PropertyDefinition<T>
{
    /// <summary>
    /// The name of the property in GObject/C.
    /// </summary>
    public string UnmanagedName { get; }

    /// <summary>
    /// The name of the property in dotnet.
    /// </summary>
    public string ManagedName { get; }

    /// <summary>
    /// Initializes a new instance of <see cref="Property{T,K}"/>.
    /// </summary>
    /// <param name="unmanagedName">The GObject/C name of this property.</param>
    /// <param name="managedName">The dotnet name of this property.</param>
    public Property(string unmanagedName, string managedName)
    {
        UnmanagedName = unmanagedName;
        ManagedName = managedName;
    }

    /// <summary>
    /// Get the value of this property in the given object.
    /// </summary>
    public T Get(K obj)
    {
        if (obj is not Object o)
            throw new Exception($"Can't get property {ManagedName} for object of type {typeof(K).Name} as it is not derived from {nameof(Object)}.");

        using var value = new Value();
        o.GetProperty(UnmanagedName, value);
        
        return value.Extract<T>();
    }

    /// <summary>
    /// Set the value of this property in the given object
    /// using the given value.
    /// </summary>
    public void Set(K obj, T value)
    {
        if (obj is not Object o)
            throw new Exception($"Can't set property {ManagedName} for object of type {typeof(K).Name} as it is not derived from {nameof(Object)}.");

        var type = GetPropertyType(o.Handle);
        using var gvalue = new Value(type);
        gvalue.Set(value);
        Internal.Object.SetProperty(o.Handle, GLib.Internal.NonNullableUtf8StringOwnedHandle.Create(UnmanagedName), gvalue.Handle);
    }

    private Type GetPropertyType(IntPtr handle)
    {
        var instance = Marshal.PtrToStructure<Internal.ObjectData>(handle);
        var classPtr = instance.GTypeInstance.GClass;
        var paramSpecPtr = Internal.ObjectClass.FindProperty(new Internal.ObjectClassUnownedHandle(classPtr), GLib.Internal.NonNullableUtf8StringOwnedHandle.Create(UnmanagedName));
        var paramSpec = Marshal.PtrToStructure<Internal.ParamSpecData>(paramSpecPtr);

        return new Type(paramSpec.ValueType);
    }
}
