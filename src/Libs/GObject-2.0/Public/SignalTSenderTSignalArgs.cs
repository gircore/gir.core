namespace GObject;

/// <summary>
/// Describes a GSignal.
/// </summary>
public class Signal<TSender, TSignalArgs> : SignalDefinition
    where TSender : Object
    where TSignalArgs : SignalArgs, new()
{
    public string UnmanagedName { get; }
    public string ManagedName { get; }

    public Signal(string unmanagedName, string managedName)
    {
        UnmanagedName = unmanagedName;
        ManagedName = managedName;
    }

    /// <summary>
    /// Connects an <paramref name="action"/> to this signal.
    /// </summary>
    /// <param name="o">The object on which connect the handler.</param>
    /// <param name="signalHandler">The signal handler function.</param>
    /// <param name="after">
    /// Define if this action must be called before or after the default handler of this signal.
    /// </param>
    public void Connect(TSender o, SignalHandler<TSender, TSignalArgs> signalHandler, bool after = false)
    {
        var closure = new Closure((returnValue, parameters) =>
        {
            var args = new TSignalArgs();
            args.SetArgs(parameters);
            signalHandler(o, args);
        });

        o.SignalConnectClosure(this, signalHandler, closure, after);
    }

    /// <summary>
    /// Disconnects an <paramref name="action"/> previously connected to this signal.
    /// </summary>
    /// <param name="o">The object from which disconnect the handler.</param>
    /// <param name="signalHandler">The signal handler function.</param>
    public void Disconnect(TSender o, SignalHandler<TSender, TSignalArgs> signalHandler)
    {
        o.Disconnect(this, signalHandler);
    }
}
