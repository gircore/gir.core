namespace Gtk
{
    public partial class Frame
    {
        public static Frame New(string label)
            => new Frame(Internal.Frame.Instance.Methods.New(label), false);
    }
}
