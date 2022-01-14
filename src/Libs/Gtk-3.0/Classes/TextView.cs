namespace Gtk
{
    public partial class TextView
    {
        public static TextView New()
            => new(Internal.TextView.New(), false);

        public static TextView NewWithBuffer(TextBuffer buffer)
            => new(Internal.TextView.NewWithBuffer(buffer.Handle), false);
    }
}
