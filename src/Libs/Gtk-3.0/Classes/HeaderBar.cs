namespace Gtk
{
    public partial class HeaderBar
    {
        public static HeaderBar New()
            => new HeaderBar(Internal.HeaderBar.Instance.Methods.New(), false);
    }
}
