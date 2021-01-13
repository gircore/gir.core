namespace Gdk
{   
    public partial class Screen
    {
        public static Screen? GetDefault()
            => WrapNullableHandle<Screen>(Native.get_default(), false);
    }
}
