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
        var gType = TSender.GetGType();

        return Internal.Functions.SignalLookup(GLib.Internal.NonNullableUtf8StringOwnedHandle.Create(UnmanagedName), gType);
    }

    /// <summary>
    /// Connects a <paramref name="signalHandler"/> to this signal.
    /// </summary>
    /// <param name="sender">The object on which connect the handler.</param>
    /// <param name="signalHandler">The signal handler function.</param>
    /// <param name="after">Define if this action must be called before or after the default handler of this signal.</param>
    /// <param name="detail">Define for which signal detail the connection should be made.</param>
    public void Connect(TSender sender, SignalHandler<TSender> signalHandler, bool after = false, string? detail = null)
    {
        var closure = new Closure(
            callback: (returnValue, parameters) => signalHandler(sender, System.EventArgs.Empty),
            handle: sender.Handle
        );

        sender.SignalConnectClosure(this, signalHandler, closure, after, detail);
    }

    /// <summary>
    /// Disconnects a <paramref name="signalHandler"/> previously connected to this signal.
    /// </summary>
    /// <param name="sender">The object from which disconnect the handler.</param>
    /// <param name="signalHandler">The signal handler function.</param>
    public void Disconnect(TSender sender, SignalHandler<TSender> signalHandler)
    {
        sender.Disconnect(this, signalHandler);
    }
}
