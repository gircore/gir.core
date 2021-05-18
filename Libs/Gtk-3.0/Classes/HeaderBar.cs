namespace Gtk
{
    public partial class HeaderBar
    {
        public static HeaderBar New()
            => new HeaderBar(Native.HeaderBar.Instance.Methods.New(), false);
    }
}