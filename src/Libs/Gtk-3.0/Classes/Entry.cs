namespace Gtk
{
    public partial class Entry
    {
        public static Entry New()
            => new Entry(Internal.Entry.New(), false);
    }
}
