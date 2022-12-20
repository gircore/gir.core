namespace GObject;

public interface PropertyDefinition<T>
{
    /// <summary>
    /// The name of the property in GObject/C.
    /// </summary>
    public string UnmanagedName { get; }

    /// <summary>
    /// The name of the property in dotnet.
    /// </summary>
    public string ManagedName { get; }
}
