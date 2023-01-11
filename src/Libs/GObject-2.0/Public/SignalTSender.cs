namespace GObject;

/// <summary>
/// Describes a GSignal.
/// </summary>
public class Signal<TSender> : SignalDefinition
    where TSender : Object, GTypeProvider
{
    private uint? _id;

    public string UnmanagedName { get; }
    public string ManagedName { get; }
    public uint Id => _id ??= GetId();

    public Signal(string unmanagedName, string managedName)
    {
        UnmanagedName = unmanagedName;
        ManagedName = managedName;
    }

    private uint GetId()
    {
#if NET7_0_OR_GREATER
        var gType = TSender.GetGType();
#else
        var gType = Internal.GTypeProviderHelper.GetGType<TSender>();
#endif

        return Internal.Functions.SignalLookup(UnmanagedName, gType);
    }

    /// <summary>
    /// Connects a <paramref name="signalHandler"/> to this signal.
    /// </summary>
    /// <param name="o">The object on which connect the handler.</param>
    /// <param name="signalHandler">The signal handler function.</param>
    /// <param name="after">Define if this action must be called before or after the default handler of this signal.</param>
    /// <param name="detail">Define for which signal detail the connection should be made.</param>
    public void Connect(TSender o, SignalHandler<TSender> signalHandler, bool after = false, string? detail = null)
    {
        var closure = new Closure((returnValue, parameters) => signalHandler(o, System.EventArgs.Empty));

        o.SignalConnectClosure(this, signalHandler, closure, after, detail);
    }

    /// <summary>
    /// Disconnects a <paramref name="signalHandler"/> previously connected to this signal.
    /// </summary>
    /// <param name="o">The object from which disconnect the handler.</param>
    /// <param name="signalHandler">The signal handler function.</param>
    public void Disconnect(TSender o, SignalHandler<TSender> signalHandler)
    {
        o.Disconnect(this, signalHandler);
    }
}
