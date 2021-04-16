namespace Gtk
{
    public partial class AboutDialog
    {
        public AboutDialog() : this(Native.AboutDialog.Instance.Methods.New(), false){ }
    }
}
