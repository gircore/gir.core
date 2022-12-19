using System;

namespace GObject;

/// <summary>
/// Describes a GProperty.
/// </summary>
/// <typeparam name="T">The type of the value this property will store.</typeparam>
/// <typeparam name="K">The type of the class / interface implementing this property.</typeparam>
public sealed class Property<T, K> : PropertyDefinition<T>
{
    private readonly Func<K, T>? _get;
    private readonly Action<K, T>? _set;

    /// <summary>
    /// The name of the property in GObject/C.
    /// </summary>
    public string UnmanagedName { get; }

    /// <summary>
    /// The name of the property in dotnet.
    /// </summary>
    public string ManagedName { get; }

    /// <summary>
    /// Checks if this GProperty is readable.
    /// </summary>
    public bool IsReadable => _get != null;

    /// <summary>
    /// Checks if this GProperty is writeable.
    /// </summary>
    public bool IsWriteable => _set != null;

    /// <summary>
    /// Initializes a new instance of <see cref="Property{T,K}"/>.
    /// </summary>
    /// <param name="unmanagedName">The GObject/C name of this property.</param>
    /// <param name="managedName">The dotnet name of this property.</param>
    /// <param name="get">The function called when retrieving the value of this property in bindings.</param>
    /// <param name="set">The function called when defining the value of this property in bindings.</param>
    public Property(string unmanagedName, string managedName, Func<K, T>? get = null, Action<K, T>? set = null)
    {
        UnmanagedName = unmanagedName;
        ManagedName = managedName;
        _get = get;
        _set = set;
    }

    /// <summary>
    /// Get the value of this property in the given object.
    /// </summary>
    public T Get(K o) => _get is null
        ? throw new InvalidOperationException("Trying to read the value of a write-only property.")
        : _get(o);

    /// <summary>
    /// Set the value of this property in the given object
    /// using the given value.
    /// </summary>
    public void Set(K o, T v)
    {
        if (_set is null)
            throw new InvalidOperationException("Trying to write the value of a read-only property.");

        _set(o, v);
    }
}
