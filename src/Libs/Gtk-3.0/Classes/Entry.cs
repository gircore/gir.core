namespace Gtk
{
    public partial class Entry
    {
        public static Entry New()
            => new Entry(Native.Entry.Instance.Methods.New(), false);
    }
}
