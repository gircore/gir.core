using System;

namespace GtkClutter
{
    public static class GtkApplicationExtensions
    {
        public static void InitClutter(this Gtk.Application application)
        {
            var ptr = IntPtr.Zero;
            var i = 0;
            Sys.Methods.init(ref i, ref ptr);
        }
    }
}