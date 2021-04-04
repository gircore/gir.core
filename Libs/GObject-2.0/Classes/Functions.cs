namespace GObject
{
    public partial class Functions
    {
        internal static bool TypeIsA(Types type, Types isAType)
            => Native.Functions.TypeIsA((ulong) type, (ulong) isAType);
    }
}
