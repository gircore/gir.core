using System;

namespace GdkPixbuf.Core
{
    public class Pixbuf : GObject.Core.GObject
    {
        internal Pixbuf(IntPtr handle) : base(handle) { }
    }
}