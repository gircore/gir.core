using GObject.Internal;

namespace Gtk
{
    public partial class Label
    {
        public static Label New(string text)
            => new(Internal.Label.Instance.Methods.New(text), false);
    }
}
