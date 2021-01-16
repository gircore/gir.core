namespace Gdk
{   
    public partial class Screen
    {
        public static Screen? GetDefault()
            => Wrapper.WrapNullableHandle<Screen>(Native.get_default(), false);
    }
}
