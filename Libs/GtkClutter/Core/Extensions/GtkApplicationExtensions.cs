using System;

namespace GtkClutter.Core
{
    public static class GtkApplicationExtensions
    {
        public static void InitClutter(this Gtk.Core.GApplication application)
        {
            var ptr = IntPtr.Zero;
            int i = 0;
            GtkClutter.Methods.init(ref i, ref ptr);
        }
    }
}