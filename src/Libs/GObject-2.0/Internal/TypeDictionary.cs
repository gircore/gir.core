using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GObject.Internal;

/// <summary>
/// This exception is thrown when a <see cref="System.Type"/> is not found in
/// the type dictionary.
/// </summary>
public class TypeNotFoundException : Exception
{
    public TypeNotFoundException(System.Type managedType)
        : base($"Type {managedType.FullName} not registered in type dictionary") { }
}

public enum BasicType
{
    Invalid = 0 << 2,
    None = 1 << 2,
    Interface = 2 << 2,
    Char = 3 << 2,
    UChar = 4 << 2,
    Boolean = 5 << 2,
    Int = 6 << 2,
    UInt = 7 << 2,
    Long = 8 << 2,
    ULong = 9 << 2,
    Int64 = 10 << 2,
    UInt64 = 11 << 2,
    Enum = 12 << 2,
    Flags = 13 << 2,
    Float = 14 << 2,
    Double = 15 << 2,
    String = 16 << 2,
    Pointer = 17 << 2,
    Boxed = 18 << 2,
    Param = 19 << 2,
    Object = 20 << 2,
    Variant = 21 << 2
}

/// <summary>
/// The global type dictionary which maps between the .NET Type System and
/// the GType dynamic type system.
/// </summary>
public static class TypeDictionary
{
    private static readonly Dictionary<System.Type, Type> _systemTypeDict = new();

    /// <summary>
    /// Add a new mapping of (System.Type, GObject.Type) to the type dictionary. 
    /// </summary>
    /// <param name="systemType">A managed type that has not already been registered</param>
    /// <param name="type">The gtype retrieved from the object's get type method or from registration.</param>
    public static void Add(System.Type systemType, Type type)
    {
        // Check we have not already registered
        Debug.Assert(
            condition: !_systemTypeDict.ContainsKey(systemType),
            message: $"Type {nameof(systemType)} is already registered in the type dictionary."
        );

        _systemTypeDict[systemType] = type;
    }

    /// <summary>
    /// For a given managed GObject-based class, retrieve the corresponding gtype.
    /// </summary>
    /// <param name="type">The type of a class that is equal or derived from <see cref="GObject.Object"/></param>
    /// <returns>The equivalent GType</returns>
    /// <exception cref="TypeNotFoundException">The given type is not registered in the type dictionary. The caller should register it themselves.</exception>
    internal static Type GetGType(System.Type type)
    {
        // Check we are a GObject
        Debug.Assert(
            condition: type.IsAssignableTo(typeof(GObject.Object)),
            message: $"Parameter {type} is not a GObject or subclass of GObject"
        );

        // Throw if type is not registered
        if (!_systemTypeDict.ContainsKey(type))
            throw new TypeNotFoundException(type);

        return _systemTypeDict[type];
    }

    // These may be unneeded - keep for now 
    internal static bool ContainsSystemType(System.Type type)
        => _systemTypeDict.ContainsKey(type);
}
