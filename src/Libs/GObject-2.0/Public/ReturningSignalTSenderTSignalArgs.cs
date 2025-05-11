namespace GObject;

/// <summary>
/// Describes a GSignal.
/// </summary>
public class ReturningSignal<TSender, TSignalArgs, TReturn> : SignalDefinition
    where TSender : Object, GTypeProvider
    where TSignalArgs : SignalArgs, new()
{
    private uint? _id;

    public string UnmanagedName { get; }
    public string ManagedName { get; }
    public uint Id => _id ??= GetId();

    public ReturningSignal(string unmanagedName, string managedName)
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
    public void Connect(TSender sender, ReturningSignalHandler<TSender, TSignalArgs, TReturn> signalHandler, bool after = false, string? detail = null)
    {
        var closure = new Closure(
            callback: (returnValue, parameters) =>
            {
                if (returnValue is null)
                    throw new System.Exception($"{nameof(TSender)}.{ManagedName}: C did not provide a value pointer to return the signal result");

                var args = new TSignalArgs();
                args.SetArgs(parameters);

                var result = signalHandler(sender, args);
                returnValue.Set(result);
            },
            handle: sender.Handle
        );

        sender.Handle.Connect(this, signalHandler, closure, after, detail);
    }

    /// <summary>
    /// Disconnects a <paramref name="signalHandler"/> previously connected to this signal.
    /// </summary>
    /// <param name="sender">The object from which disconnect the handler.</param>
    /// <param name="signalHandler">The signal handler function.</param>
    public void Disconnect(TSender sender, ReturningSignalHandler<TSender, TSignalArgs, TReturn> signalHandler)
    {
        sender.Handle.Disconnect(this, signalHandler);
    }
}
