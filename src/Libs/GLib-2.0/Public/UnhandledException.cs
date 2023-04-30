using System;

namespace GLib;

/// <summary>
/// Allows handlings exceptions which can't cross the native code boundary and
/// would terminate the application.
/// </summary>
public static class UnhandledException
{
    /// <summary>
    /// Invoked if an exception is raised which reaches the native code boundary.
    /// This can be used by applications to handle these exceptions gracefully
    /// without terminating the application.
    /// </summary>
    public static event EventHandler<UnhandledExceptionEventArgs>? Raised;

    /// <summary>
    /// Call this method to invoke the "Raised" event. If the event has no subscribers,
    /// the application is terminated.
    /// </summary>
    /// <remarks>
    /// This method is not intended to be called by application code, and should only
    /// be called by code which catches an exception that would otherwise terminate
    /// the application by unwinding to the native code boundary.
    /// </remarks>
    public static void Raise(Exception e)
    {
        if (Raised is null)
        {
            Console.Error.WriteLine($"{nameof(GLib.UnhandledException)} - unhandled exception: {e}");
            Environment.Exit(1);
        }
        else
        {
            Raised.Invoke(null, new UnhandledExceptionEventArgs(e, false));
        }
    }
}
