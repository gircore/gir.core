using System;

namespace GObject;

/// <summary>
/// Define the value of GProperty which can be used at the construct time.
/// </summary>
public sealed class ConstructArgument : IDisposable
{
    /// <summary>
    /// The GProperty name to set at the construct time.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// The value of the property.
    /// </summary>
    public Value Value { get; }

    public ConstructArgument(string name, Value value)
    {
        Name = name;
        Value = value;
    }

    public void Dispose()
    {
        Value.Dispose();
    }
}
