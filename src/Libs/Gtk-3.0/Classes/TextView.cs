using System;
using GObject.Internal;

namespace Gtk
{
    public partial class TextView
    {
        public static TextView New()
            => new(Internal.TextView.Instance.Methods.New(), false);

        public static TextView NewWithBuffer(TextBuffer buffer)
            => new(Internal.TextView.Instance.Methods.NewWithBuffer(buffer.Handle), false);
    }
}
