namespace GObject
{
    public partial class Functions
    {
        internal static bool TypeIsA(Types type, Types isAType)
            => Native.TypeIsA((ulong) type, (ulong) isAType);
    }
}
