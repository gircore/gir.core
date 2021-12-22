namespace Gtk
{
    public partial class DrawingArea
    {
        public static DrawingArea New()
            => new DrawingArea(Internal.DrawingArea.Instance.Methods.New(), false);
    }
}
