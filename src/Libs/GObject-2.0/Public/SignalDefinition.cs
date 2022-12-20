namespace GObject;

public interface SignalDefinition
{
    /// <summary>
    /// The name of the signal in GObject/C.
    /// </summary>
    public string UnmanagedName { get; }

    /// <summary>
    /// The name of the signal in dotnet.
    /// </summary>
    public string ManagedName { get; }
}
