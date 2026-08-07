using System.Collections.Generic;

namespace GirModel;

public interface Parameter : Nullable
{
    string Name { get; }
    Direction Direction { get; }
    Transfer Transfer { get; }
    bool Optional { get; }
    bool CallerAllocates { get; }
    int? Closure { get; }
    int? Destroy { get; }
    bool IsPointer { get; }
    bool IsConst { get; }
    bool IsVolatile { get; }
    OneOf.OneOf<AnyType, VarArgs> AnyTypeOrVarArgs { get; }

    /// <summary>
    /// The resolved element types if this parameter is a container type
    /// like GLib.List. GLib.HashTable has two element types: the key and the
    /// value type. Empty if there are no (resolved) element types.
    /// </summary>
    IReadOnlyList<AnyType> ElementTypes { get; }
    Scope? Scope { get; }
}
