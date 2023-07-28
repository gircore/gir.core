namespace GLib.Internal;

public interface NativeGTypeProvider
{
#if NET7_0_OR_GREATER
    static abstract nuint GetGType();
#endif
}
