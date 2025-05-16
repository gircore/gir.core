using System.Diagnostics;

namespace GObject;

/// <summary>
/// Describes a GSignal.
/// </summary>
public class Signal<TSender, TSignalArgs> : SignalDefinition
    where TSender : Object, GTypeProvider
    where TSignalArgs : SignalArgs, new()
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
    public void Connect(TSender sender, SignalHandler<TSender, TSignalArgs> signalHandler, bool after = false, string? detail = null)
    {
        var closure = sender.Handle.GetClosure(signalHandler, () => new Closure((returnValue, parameters) =>
        {
            var args = new TSignalArgs();
            args.SetArgs(parameters);
            signalHandler(sender, args);
        }));
        closure.Connect(sender.Handle, this, after, detail);
    }

    /// <summary>
    /// Disconnects a <paramref name="signalHandler"/> previously connected to this signal.
    /// </summary>
    /// <param name="sender">The object from which disconnect the handler.</param>
    /// <param name="signalHandler">The signal handler function.</param>
    public void Disconnect(TSender sender, SignalHandler<TSender, TSignalArgs> signalHandler)
    {
        if (!sender.Handle.TryGetClosure(signalHandler, out var closure))
        {
            Debug.Fail($"Handle {sender.Handle.DangerousGetHandle()}: Could not disconnect from signal {ManagedName}. No matching closure found for signal handler.");
            return;
        }

        closure.Disconnect(sender.Handle, this);
    }
}
