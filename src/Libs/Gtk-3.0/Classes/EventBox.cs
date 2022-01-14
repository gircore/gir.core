namespace Gtk
{
    public partial class EventBox
    {
        public static EventBox New()
            => new EventBox(Internal.EventBox.New(), false);
    }
}
