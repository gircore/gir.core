namespace Gtk
{
    public partial class Label
    {
        public static Label New(string text)
            => new(Internal.Label.New(text), false);
    }
}
