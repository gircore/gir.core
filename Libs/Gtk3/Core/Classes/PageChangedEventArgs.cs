using System;

namespace Gtk.Core
{
    public class PageChangedEventArgs : EventArgs
    {
        public GWidget Child { get; }
        public uint PageNum { get; }

        public PageChangedEventArgs(GWidget child, uint pagenum)
        {
            Child = child ?? throw new ArgumentNullException(nameof(child));
            PageNum = pagenum;
        }
    }
}