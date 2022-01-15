namespace Gtk
{
    public partial class Expander
    {
        public static Expander New(string text)
            => new(Internal.Expander.New(text), false);
    }
}
