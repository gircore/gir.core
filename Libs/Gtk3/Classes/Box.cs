namespace Gtk
{
    public partial class Box
    {
        #region Methods

        public void PackStart(Widget widget, bool expand, bool fill, uint padding) 
            => Native.pack_start(Handle, GetHandle(widget), expand, fill, padding);

        #endregion
    }
}
