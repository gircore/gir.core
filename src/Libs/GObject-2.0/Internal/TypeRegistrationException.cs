using System;

namespace GObject.Internal;

/// <summary>
/// Thrown when type registration with GType fails
/// </summary>
public class TypeRegistrationException : Exception
{
    internal TypeRegistrationException(string message) : base(message) { }
}
