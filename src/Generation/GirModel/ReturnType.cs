using System.Collections.Generic;

namespace GirModel;

public interface ReturnType : Nullable
{
    AnyType AnyType { get; }

    /// <summary>
    /// The resolved element types if <see cref="AnyType"/> is a container type
    /// like GLib.List. GLib.HashTable has two element types: the key and the
    /// value type. Empty if there are no (resolved) element types.
    /// </summary>
    IReadOnlyList<AnyType> ElementTypes { get; }
    Transfer Transfer { get; }
    bool IsPointer { get; }
}
