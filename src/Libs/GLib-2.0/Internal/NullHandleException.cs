using System;

namespace GLib.Internal;

/// <summary>
/// This exception is raised if an unexpeected NULL handle is received. Please report any occurence of this
/// exception to the gir.core project as it is likely to be a generation error.
/// </summary>
public class NullHandleException : Exception
{
    public NullHandleException(string message) : base(message) { }
}
