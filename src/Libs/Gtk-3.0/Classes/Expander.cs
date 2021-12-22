using GObject.Internal;

namespace Gtk
{
    public partial class Expander
    {
        public static Expander New(string text)
            => new(Internal.Expander.Instance.Methods.New(text), false);
    }
}
