using GObject.Internal;

namespace GObject;

public interface NativeObject
{
    ObjectHandle Handle { get; }
}
