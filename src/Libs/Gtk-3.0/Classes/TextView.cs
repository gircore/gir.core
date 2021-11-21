using System;
using GObject.Internal;

namespace Gtk
{
    public partial class TextView
    {
        public static TextView New()
            => new(Native.TextView.Instance.Methods.New(), false);

        public static TextView NewWithBuffer(TextBuffer buffer)
            => new(Native.TextView.Instance.Methods.NewWithBuffer(buffer.Handle), false);
    }
}
