using System;

namespace Gtk
{
    public class PageChangedEventArgs : EventArgs
    {
        public Widget Child { get; }
        public uint PageNum { get; }

        public PageChangedEventArgs(Widget child, uint pagenum)
        {
            Child = child ?? throw new ArgumentNullException(nameof(child));
            PageNum = pagenum;
        }
    }
}