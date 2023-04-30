using System;

namespace GLib;

/// <summary>
/// Allows handling exceptions which can't cross the native code boundary and
/// would terminate the application.
/// </summary>
public static class UnhandledException
{
    private static Action<Exception>? ExceptionHandler;

    /// <summary>
    /// Sets an action which is invoked if an exception is raised which reaches the native code boundary.
    /// This can be used by applications to handle these exceptions gracefully
    /// without terminating the application.
    /// </summary>
    /// <param name="handler">Invoked if an unhandled exception occurs</param>
    public static void SetHandler(Action<Exception>? handler)
    {
        ExceptionHandler = handler;
    }

    /// <summary>
    /// Call this method to invoke the exception handler. If there is no exception
    /// handler registered the application is terminated.
    /// </summary>
    /// <remarks>
    /// This method is not intended to be called by application code, and should only
    /// be called by code which catches an exception that would otherwise terminate
    /// the application by unwinding to the native code boundary.
    /// </remarks>
    public static void Raise(Exception e)
    {
        if (ExceptionHandler is null)
        {
            Console.Error.WriteLine($"{nameof(GLib.UnhandledException)} - unhandled exception: {e}");
            Environment.Exit(1);
        }
        else
        {
            ExceptionHandler(e);
        }
    }
}
