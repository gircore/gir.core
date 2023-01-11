namespace GObject;

public interface SignalDefinition
{
    /// <summary>
    /// The name of the signal in GObject/C.
    /// </summary>
    string UnmanagedName { get; }

    /// <summary>
    /// The name of the signal in dotnet.
    /// </summary>
    string ManagedName { get; }

    /// <summary>
    /// The Id of the signal in GObject/C
    /// </summary>
    uint Id { get; }
}
