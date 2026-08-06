using System.Collections.Generic;

namespace GirModel;

public interface ElementTypeContainer
{
    /// <summary>
    /// The element types of the type: container types like GLib.List carry
    /// their element type here, GLib.HashTable carries two (the key and the
    /// value type) and arrays carry exactly one. Empty if the type has no
    /// element types.
    /// </summary>
    IReadOnlyList<AnyType> ElementTypes { get; }
}
