using System;

namespace GObject;

/// <summary>
/// Describes a GSignal.
/// </summary>
public class Signal<TSender> : SignalDefinition
    where TSender : Object
{
    public string UnmanagedName { get; }
    public string ManagedName { get; }

    public Signal(string unmanagedName, string managedName)
    {
        UnmanagedName = unmanagedName;
        ManagedName = managedName;
    }

    /// <summary>
    /// Connects an <paramref name="signalHandler"/> to this signal.
    /// </summary>
    /// <param name="o">The object on which connect the handler.</param>
    /// <param name="signalHandler">The signal handler function.</param>
    /// <param name="after">
    /// Define if this action must be called before or after the default handler of this signal.
    /// </param>
    public void Connect(TSender o, SignalHandler<TSender> signalHandler, bool after = false)
    {
        var closure = new Closure((returnValue, parameters) => signalHandler(o, EventArgs.Empty));

        o.SignalConnectClosure(this, signalHandler, closure, after);
    }

    /// <summary>
    /// Disconnects an <paramref name="signalHandler"/> previously connected to this signal.
    /// </summary>
    /// <param name="o">The object from which disconnect the handler.</param>
    /// <param name="signalHandler">The signal handler function.</param>
    public void Disconnect(TSender o, SignalHandler<TSender> signalHandler)
    {
        o.Disconnect(this, signalHandler);
    }
}
