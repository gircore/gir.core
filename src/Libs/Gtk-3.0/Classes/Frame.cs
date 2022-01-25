namespace Gtk
{
    public partial class Frame
    {
        public static Frame New(string label)
            => new Frame(Internal.Frame.New(label), false);
    }
}
