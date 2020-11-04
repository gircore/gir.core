namespace Gtk
{
    public partial class Widget
    {
        #region Methods
        public void ShowAll() => Native.show_all(Handle);
        #endregion
    }
}
