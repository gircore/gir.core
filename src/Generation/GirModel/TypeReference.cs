using System.Collections.Generic;

namespace GirModel;

public interface TypeReference
{
    /// <summary>
    /// The referenced type.
    /// </summary>
    Type Type { get; }

    /// <summary>
    /// The element types of the type. Type references to container types
    /// can contain one or more element types depending on the container
    /// type (e.g., GLib.List or GLib.HashTable).
    /// </summary>
    IReadOnlyList<AnyType> ElementTypes { get; }
}