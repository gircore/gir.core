using System;
using System.Linq;

namespace GObject.Internal;

public partial class Object
{
    public static IntPtr NewWithProperties(nuint objectType, ConstructArgument[] constructArguments)
    {
        return NewWithProperties(
            objectType: objectType,
            nProperties: (uint) constructArguments.Length,
            names: GLib.Internal.Utf8StringArraySizedOwnedHandle.Create(constructArguments.Select(x => x.Name).ToArray()),
            values: ValueArray2OwnedHandle.Create(constructArguments.Select(x => x.Value).ToArray())
        );
    }
}
