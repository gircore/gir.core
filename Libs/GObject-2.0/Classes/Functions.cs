namespace GObject
{
    public partial class Functions
    {
        internal static bool TypeIsA(ulong type, Types isAType)
            => Native.Functions.TypeIsA(type, (ulong) isAType);
    }
}
