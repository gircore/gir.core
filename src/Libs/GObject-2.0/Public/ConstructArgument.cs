using System;

namespace GObject;

/// <summary>
/// Define the value of GProperty which can be used at the construct time.
/// </summary>
public sealed class ConstructArgument(string name, Value value) : IDisposable
{
    /// <summary>
    /// The GProperty name to set at the construct time.
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    /// The value of the property.
    /// </summary>
    public Value Value { get; } = value;

    public void Dispose()
    {
        Value.Dispose();
    }
}
