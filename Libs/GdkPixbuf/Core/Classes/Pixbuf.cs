using System;

namespace GdkPixbuf
{
    public class Pixbuf : GObject.Object
    {
        internal Pixbuf(IntPtr handle) : base(handle) { }
    }
}