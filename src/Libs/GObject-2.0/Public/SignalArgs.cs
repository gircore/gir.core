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

    protected static T Extract<T>(Value value) => value.Extract<T>();
    protected static T[] ExtractArray<T>(Value value, int numberElements) where T : GObject.Object
    {
        return value.ExtractArray<T>(numberElements);
    }
}
