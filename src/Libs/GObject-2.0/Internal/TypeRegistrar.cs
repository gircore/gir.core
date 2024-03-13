using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GObject.Internal;

/// <summary>
/// Thrown when type registration with GType fails
/// </summary>
internal class TypeRegistrationException : Exception
{
    public TypeRegistrationException(string message) : base(message) { }
}

/// <summary>
/// A set of utility functions to register new types with the
/// GType dynamic type system.
/// </summary>
public static class TypeRegistrar
{
    /// <summary>
    /// Registers with GType a new child class of 'parentType'.
    /// </summary>
    /// <param name="qualifiedName">The name of the class</param>
    /// <param name="parentType">The parent class to derive from</param>
    /// <returns>The newly registered type</returns>
    /// <exception cref="TypeRegistrationException">The type could not be registered</exception>
    internal static Type RegisterGType(string qualifiedName, Type parentType)
    {
        var typeQuery = TypeQueryOwnedHandle.Create();
        Functions.TypeQuery(parentType, typeQuery);

        if (typeQuery.GetType() == 0)
            throw new TypeRegistrationException("Could not query parent type");

        Debug.WriteLine($"Registering new type {qualifiedName} with parent {parentType.ToString()}");

        // Create TypeInfo
        //TODO: Callbacks for "ClassInit" and "InstanceInit" are disabled because if multiple instances
        //of the same type are created, the typeInfo object can get garbagec collected in the mean time
        //and with it the instances of "DoClassInit" and "DoInstanceInit". If the callback occurs the
        //runtime can't do the call anymore and crashes with:
        //A callback was made on a garbage collected delegate of type 'GObject-2.0!GObject.Internal.InstanceInitFunc::Invoke'
        //Fix this by caching the garbage collected instances somehow
        var handle = TypeInfoOwnedHandle.Create();
        handle.SetClassSize((ushort) typeQuery.GetClassSize());
        handle.SetInstanceSize((ushort) typeQuery.GetInstanceSize());
        //handle.SetClassInit();
        //handle.SetInstanceInit();

        var typeid = Functions.TypeRegisterStatic(parentType, GLib.Internal.NonNullableUtf8StringOwnedHandle.Create(qualifiedName), handle, 0);

        if (typeid == 0)
            throw new TypeRegistrationException("Type Registration Failed!");

        return new Type(typeid);
    }

    /* TODO: Enable if init functions are supported again
    // Default Handler for class initialisation.
    private static void DoClassInit(IntPtr gClass, IntPtr classData)
    {
        Console.WriteLine("Subclass type class initialised!");
    }

    // Default Handler for instance initialisation.
    private static void DoInstanceInit(IntPtr gClass, IntPtr classData)
    {
        Console.WriteLine("Subclass instance initialised!");
    }
    */
}
