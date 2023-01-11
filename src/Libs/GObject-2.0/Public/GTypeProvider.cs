namespace GObject;

public interface GTypeProvider
{
#if NET7_0_OR_GREATER
    static abstract nuint GetGType();
#endif
}
