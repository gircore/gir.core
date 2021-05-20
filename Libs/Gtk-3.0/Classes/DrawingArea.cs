namespace Gtk
{
    public partial class DrawingArea
    {
        public static DrawingArea New()
            => new DrawingArea(Native.DrawingArea.Instance.Methods.New(), false);
    }
}