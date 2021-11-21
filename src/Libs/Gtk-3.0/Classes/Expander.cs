using GObject.Internal;

namespace Gtk
{
    public partial class Expander
    {
        public static Expander New(string text)
            => new(Native.Expander.Instance.Methods.New(text), false);
    }
}
