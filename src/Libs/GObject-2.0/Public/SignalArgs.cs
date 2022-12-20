using System;

namespace GObject;

/// <summary>
/// Base class for signal based events.
/// </summary>
public class SignalArgs : EventArgs
{
    protected Value[] Args { get; set; }

    protected SignalArgs()
    {
        Args = Array.Empty<Value>();
    }

    internal void SetArgs(Value[] args)
    {
        Args = args;
    }
}
