namespace Gtk
{
    public partial class Frame
    {
        public static Frame New(string label)
            => new Frame(Native.Frame.Instance.Methods.New(label), false);
    }
}
