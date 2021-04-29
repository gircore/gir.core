using GObject.Native;

namespace Gtk
{
    public partial class Expander
    {
        public static Expander New(string text)
            => ObjectWrapper.WrapHandle<Expander>(Native.Expander.Instance.Methods.New(text), false);
    }
}
