using System;
using GObject.Native;

namespace Gtk
{
    public partial class TextView
    {
        public static TextView New()
            => ObjectWrapper.WrapHandle<TextView>(Native.TextView.Instance.Methods.New(), false);
        
        public static TextView NewWithBuffer(TextBuffer buffer)
            => ObjectWrapper.WrapHandle<TextView>(Native.TextView.Instance.Methods.NewWithBuffer(buffer.Handle), false);
    }
}
