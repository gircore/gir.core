namespace Gtk
{
    public partial class Box
    {
        #region Constructors
        
        public Box(Orientation orientation, int spacing) : this(Native.@new(orientation, spacing)) { }
        
        #endregion
        
        #region Methods

        public void PackStart(Widget widget, bool expand, bool fill, uint padding) 
            => Native.pack_start(Handle, GetHandle(widget), expand, fill, padding);

        #endregion
    }
}
