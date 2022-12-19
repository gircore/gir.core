using System;
using System.Runtime.InteropServices;

namespace GObject;

public partial class Object
{
    #region Methods

    /// <summary>
    /// Gets the value of the GProperty described by <paramref name="property"/>.
    /// </summary>
    /// <param name="property">The property descriptor of the GProperty from which get the value.</param>
    /// <typeparam name="T">The type of the value to retrieve.</typeparam>
    /// <returns>
    /// The value of the GProperty.
    /// </returns>
    protected T GetProperty<T>(PropertyDefinition<T> property)
        => GetProperty(property.UnmanagedName).Extract<T>();

    /// <summary>
    /// Sets the <paramref name="value"/> of the GProperty described by <paramref name="property"/>.
    /// </summary>
    /// <param name="property">The property descriptor of the GProperty on which set the value.</param>
    /// <param name="value">The value to set to the GProperty.</param>
    /// <typeparam name="T">The tye of the value to define.</typeparam>
    protected void SetProperty<T>(PropertyDefinition<T> property, T value)
    {
        using Value v = CreateValue(property.UnmanagedName);
        v.Set(value);
        SetProperty(property.UnmanagedName, v);
    }

    /// <summary>
    /// Gets the value of a GObject's property given its <paramref name="name"/>.
    /// </summary>
    /// <param name="name">The property name.</param>
    /// <returns>
    /// The native value of the property, wrapped as a <see cref="Value"/>.
    /// </returns>
    protected Value GetProperty(string name)
    {
        var handle = Internal.ValueManagedHandle.Create();
        Internal.Object.GetProperty(Handle, name, handle);

        return new Value(handle);
    }

    /// <summary>
    /// Creates a value with a type matching the property type.
    /// </summary>
    private Value CreateValue(string propertyName)
    {
        var instance = Marshal.PtrToStructure<GObject.Internal.ObjectData>(Handle);
        var classPtr = instance.GTypeInstance.GClass;
        var paramSpecPtr = FindProperty(classPtr, propertyName);
        var paramSpec = Marshal.PtrToStructure<GObject.Internal.ParamSpecData>(paramSpecPtr);

        var v = new Value(new Type(paramSpec.ValueType));
        return v;
    }

    /// <summary>
    /// TODO: Only as long as generation is not working
    /// Calls native method g_object_class_find_property.
    /// </summary>
    /// <param name="oclass">Transfer ownership: None Nullable: False</param>
    /// <param name="propertyName">Transfer ownership: None Nullable: False</param>
    /// <returns>Transfer ownership: None Nullable: False</returns>
    [DllImport("GObject", EntryPoint = "g_object_class_find_property")]
    private static extern IntPtr FindProperty(IntPtr oclass, string propertyName);

    #endregion
}
